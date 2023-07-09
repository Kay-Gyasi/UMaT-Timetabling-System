using Humanizer;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UMaTLMS.Core.Processors;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Helpers;

public static class ExamsTimetableGenerator
{
    public static List<ExamsSchedule> Generate(List<Lecture> examinableLectures, List<ClassRoom> rooms, List<SubClassGroup> subClassGroups, List<Lecturer> lecturers, List<IncomingCourse> courses, ExamsScheduleCommand command)
    {
        AppHelpers.Shuffle(examinableLectures);
        var schedules = new List<ExamsSchedule>();

        var examsGroupedByCourseNumber = GetSchedulesInInitialState(examinableLectures);
        var examPeriods = Enum.GetValues<ExamPeriod>();
        var examDates = new List<DateTime>();
        schedules = SetInitialDatesAndPeriods(command, examDates, examsGroupedByCourseNumber, examPeriods, schedules);
        schedules = AssignRooms(schedules, rooms, subClassGroups, examDates, examPeriods);
        schedules = AssignInvigilators(schedules, lecturers, courses);
        return schedules;
    }
    
    public static async Task GetAsync(IExcelReader excelReader, List<ExamsSchedule> schedules, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return;
        using var excelPackage = excelReader.CreateNew(fileName);

        var worksheets = Enum.GetValues<ExamWorksheets>();
        List<ExamsSchedule> groupedExamsSchedules;
        ExcelWorksheet worksheet;
        foreach (var examWorksheet in worksheets)
        {
            switch (examWorksheet)
            {
                case ExamWorksheets.General:
                    groupedExamsSchedules = schedules.OrderBy(x => x.DateOfExam).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelReader, excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Examiners:
                    groupedExamsSchedules = schedules.OrderBy(x => x.Examiner).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelReader, excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Invigilators:
                    groupedExamsSchedules = schedules.OrderBy(x => x.Invigilators.FirstOrDefault()?.Name).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelReader, excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.PracticalExams:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static async Task BuildWorksheet(IExcelReader excelReader, ExcelPackage excelPackage, ExcelWorksheet worksheet, 
        IEnumerable<ExamsSchedule> schedules)
    {
        BuildLayout(worksheet);

        var currentColumn = 5;
        foreach (var schedule in schedules)
        {
            if (schedule.CourseCodes is null) continue;
            var groupCount = 0;
            foreach (var code in schedule.CourseCodes)
            {
                worksheet.Cells[$"A{currentColumn}"].Value = schedule.DateOfExam.ToLongDateString();
                worksheet.Cells[$"B{currentColumn}"].Value = code.ToUpper();
                worksheet.Cells[$"C{currentColumn}"].Value = schedule.CourseName?.ToUpper();
                worksheet.Cells[$"D{currentColumn}"].Value = schedule.SubClassGroups[groupCount].Name.ToUpper();
                worksheet.Cells[$"E{currentColumn}"].Value = schedule.SubClassGroups[groupCount].Size;
                worksheet.Cells[$"F{currentColumn}"].Value = schedule.Examiner?.ToUpper();
                worksheet.Cells[$"G{currentColumn}"].Value = schedule.Room?.Name.ToUpper();
                worksheet.Cells[$"H{currentColumn}"].Value = schedule.Invigilators[groupCount].Name?.ToUpper();
                worksheet.Cells[$"I{currentColumn}"].Value = schedule.ExamPeriod switch
                {
                    ExamPeriod.Morning => "M",
                    ExamPeriod.Afternoon => "A",
                    ExamPeriod.Evening => "E",
                    _ => "-"
                };
                groupCount += 1;
                currentColumn += 1;
            }
        }

        var cells = worksheet.Cells[$"A5:I{currentColumn}"];
        cells.ApplyStyling();
        var border = worksheet.Cells[$"A5:I{currentColumn}"].Style.Border;
        border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = ExcelBorderStyle.Thin;
        await excelPackage.SaveAsync();
    }

    private static void BuildLayout(ExcelWorksheet worksheet)
    {
        // first row
        worksheet.Cells["A1"].Value = "EXAMINATIONS TIME TABLE".ToUpper();
        var cell1Range = worksheet.Cells["A1:I1"];
        cell1Range.ApplyStyling(12, isBold: true, isMerge: true);
        
        // second row
        worksheet.Cells["A2"].Value = "{Semester No.} {Academic Year}".ToUpper();
        var cell2Range = worksheet.Cells["A2:I2"];
        cell2Range.ApplyStyling(isBold: true, isMerge: true);
        
        // headers
        worksheet.Cells["A4"].Value = "Date".ToUpper();
        worksheet.Cells["B4"].Value = "Course No".ToUpper();
        worksheet.Cells["C4"].Value = "Course name".ToUpper();
        worksheet.Cells["D4"].Value = "Class".ToUpper();
        worksheet.Cells["E4"].Value = "No".ToUpper();
        worksheet.Cells["F4"].Value = "Lecturer".ToUpper();
        worksheet.Cells["G4"].Value = "Room".ToUpper();
        worksheet.Cells["H4"].Value = "Invigilator".ToUpper();
        worksheet.Cells["I4"].Value = "Period".ToUpper();
        var cell3Range = worksheet.Cells["A4:I4"];
        cell3Range.ApplyStyling(isBold: true);
        
        // column widths
        worksheet.Column(1).Width = 26.73;
        worksheet.Column(2).Width = 13.0;
        worksheet.Column(3).Width = 40.91;
        worksheet.Column(4).Width = 10.55;
        worksheet.Column(6).Width = 32.09;
        worksheet.Column(7).Width = 25.0;
        worksheet.Column(8).Width = 32.09;
    }

    private static List<IGrouping<string, ExamsSchedule>> GetSchedulesInInitialState(List<Lecture> examinableLectures)
    {
        var schedules = new List<ExamsSchedule>();
        foreach (var lecture in examinableLectures)
        {
            if (lecture.Course?.Code is null) continue;
            foreach (var group in lecture.SubClassGroups)
            {
                var scheduleIsCreated =
                    schedules.Any(x => x.CourseNo
                                       == GetCourseNumber(lecture.Course?.Code ?? string.Empty)
                                       && x.SubClassGroups.Any(a => a.Id == group.Id));
                if (scheduleIsCreated) continue;

                var courseCode = string.Join(" ", group.Name.Split(" ")[0],
                    GetCourseNumber(lecture.Course?.Code ?? string.Empty));
                var schedule = ExamsSchedule.Create(courseCode)
                    .AddGroup(group)
                    .HasInfo(lecture.LecturerId, lecture.Lecturer?.Name, lecture.Course?.Name);
                schedules.Add(schedule);
            }
        }

        return schedules.GroupBy(x => x.CourseNo)
            .OrderByDescending(a => a.Count())
            .ToList();
    }

    private static List<ExamsSchedule> SetInitialDatesAndPeriods(ExamsScheduleCommand command, List<DateTime> examDates,
        List<IGrouping<string, ExamsSchedule>> examsGroupedByCourseCode, ExamPeriod[] examPeriods,
        List<ExamsSchedule> schedules)
    {
        var date = command.StartDate;
        while (date <= command.EndDate)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday when !command.IncludeSaturdays:
                    date = date.AddDays(1);
                    continue;
                case DayOfWeek.Sunday when !command.IncludeSundays:
                    date = date.AddDays(1);
                    continue;
            }

            examDates.Add(date);
            date = date.AddDays(1);
            if (date.DayOfWeek == DayOfWeek.Saturday && !command.IncludeSaturdays)
            {
                date = date.AddDays(1);
            }

            if (date.DayOfWeek == DayOfWeek.Sunday && !command.IncludeSundays)
            { 
                date = date.AddDays(1);
            }
        }

        var examMoments = new List<(DateTime, ExamPeriod, List<int>)>();
        foreach (var examDate in examDates)
        {
            foreach (var period in examPeriods)
            {
                examMoments.Add((examDate, period, new List<int>()));
            }
        }
        
        foreach (var grouping in examsGroupedByCourseCode)
        {
            var classHasExamOnDate = true;
            var count = 0;
            var moments = examMoments;
            AppHelpers.Shuffle(moments);
            (DateTime, ExamPeriod, List<int>) moment;
            while (classHasExamOnDate)
            {
                moments = moments.OrderBy(x => x.Item3.Count).ToList();
                moment = moments[count];
                foreach (var exam in grouping)
                {
                    foreach (var group in exam.SubClassGroups)
                    {
                        classHasExamOnDate =
                            schedules.Any(x => x.DateOfExam == moment.Item1 
                                               && x.SubClassGroups.Any(a => a.Id == group.Id));
                        if (classHasExamOnDate) break;
                    }
                    
                    if (classHasExamOnDate)
                    {
                        count += 1;
                        break;
                    }
                }
                if (classHasExamOnDate) continue;
                
                foreach (var exam in grouping)
                {
                    exam.OnPeriod(moment.Item2)
                        .ToBeWrittenOn(moment.Item1);
                    schedules.Add(exam);

                    count = 0;
                    moment.Item3.AddRange(exam.SubClassGroups.Select(x => x.Id)); 
                }
            }
            
            if (grouping.Any(x => x.ExamPeriod == null))
            {
                throw new ExamDateAndPeriodNotSetException();
            }
        }

        return schedules;
    }

    private static List<ExamsSchedule> AssignRooms(List<ExamsSchedule> schedules, IEnumerable<ClassRoom> rooms,
        List<SubClassGroup> subClassGroups, List<DateTime> examDates, ExamPeriod[] examPeriods)
    {
        var roomsForGeneralExams = rooms
            .Where(x => x.IncludeInGeneralAssignment)
            .OrderBy(x => x.Capacity).ToList();
        
        foreach (var date in examDates)
        {
            foreach (var period in examPeriods)
            {
                var examsAtMoment = schedules
                    .Where(x => x.DateOfExam == date && x.ExamPeriod == period)
                    .ToList();

                while (examsAtMoment.Count > roomsForGeneralExams.Count)
                {
                    var orderedExamsAtMoment = examsAtMoment
                        .OrderBy(x => x.CourseCodes?.Count)
                        .ThenBy(x => x.Examiner)
                        .ThenBy(x => x.SerializedCourseCodes).ToList();
                    var second = examsAtMoment.First(a => a == orderedExamsAtMoment[1]);
                    var first = examsAtMoment.First(a => a == orderedExamsAtMoment[0]);
                    foreach (var group in second.SubClassGroups)
                    {
                        first.AddGroup(group, second.CourseCodes);
                    }

                    examsAtMoment.Remove(second);
                    schedules.Remove(second);
                }

                examsAtMoment = examsAtMoment.OrderByDescending(x => 
                    x.SubClassGroups.Sum(a => a.Size)).ToList();

                var assignedRooms = new List<int>();
                foreach (var exam in examsAtMoment)
                {
                    var groupSize = subClassGroups.Sum(x => x.Size);
                    var room = ClassRoom.Create("", 0);
                    var lastRoomsIndex = roomsForGeneralExams.Count - 1;
                    var hasCheckedAllRooms = false;
                    while (!hasCheckedAllRooms)
                    {
                        room = roomsForGeneralExams
                            .FirstOrDefault(x => x.Capacity >= groupSize 
                                                 && !assignedRooms.Contains(x.Id));
                        room ??= roomsForGeneralExams[lastRoomsIndex];
                        
                        if (examsAtMoment.Any(x => x.RoomId == room.Id))
                        {
                            if (lastRoomsIndex == 0) break;
                            lastRoomsIndex -= 1;
                            continue;
                        };

                        hasCheckedAllRooms = true;
                    }
                    
                    if (!hasCheckedAllRooms) throw new ExamNotAssignedRoomException();
                    exam.ToBeWrittenInRoom(room.Id);
                    assignedRooms.Add(room.Id);
                }
            }
        }

        return schedules;
    }

    private static List<ExamsSchedule> AssignInvigilators(List<ExamsSchedule> schedules, List<Lecturer> lecturers,
        List<IncomingCourse> courses)
    {
        AppHelpers.Shuffle(lecturers);
        var groupedSchedules = schedules.GroupBy(x => x.CourseNo).ToList();
        var count = 0;
        foreach (var groupedSchedule in groupedSchedules)
        {
            var invigilators = new HashSet<(string, int)>();
            foreach (var exam in groupedSchedule)
            {
                var coursesForExams = new List<IncomingCourse>();
                if (exam.CourseCodes is null) continue;
                
                foreach (var courseCode in exam.CourseCodes.Distinct())
                {
                    var course = courses.FirstOrDefault(x => x.Code == courseCode);
                    course ??= courses.FirstOrDefault(x =>
                        GetCourseNumber(x.Code ?? "") == GetCourseNumber(courseCode)
                        && x.ProgrammeCode == courseCode?.Split(" ")[0]);
                    if (course is null) continue;
                    coursesForExams.Add(course);
                }
                
                foreach (var course in coursesForExams)
                {
                    var firstExaminer = lecturers.FirstOrDefault(x => x.UmatId == course.FirstExaminerStaffId);
                    if (firstExaminer?.Name is null) continue;
                    invigilators.Add((firstExaminer.Name, firstExaminer.Id));
                    
                    var secondExaminer = lecturers.FirstOrDefault(x => x.UmatId == course.SecondExaminerStaffId);
                    if (secondExaminer?.Name is null) continue;
                    invigilators.Add((secondExaminer.Name, secondExaminer.Id));
                }
            }

            var numberOfInvigilatorsForExam = groupedSchedule.Sum(x => x.CourseCodes?.Count);
            while (invigilators.Count < numberOfInvigilatorsForExam)
            {
                var selected = lecturers[count % lecturers.Count];
                if (selected.Name is null || invigilators.Contains((selected.Name, selected.Id)))
                {
                    count += 1;
                    continue;
                }

                invigilators.Add((selected.Name, selected.Id));
                count += 1;
            }

            var newCount = 0;
            foreach (var schedule in groupedSchedule)
            {
                if (schedule.CourseCodes is null) continue;
                foreach (var _ in schedule.CourseCodes)
                {
                    var invigilator = lecturers.First(x => x.Id == invigilators.ElementAt(newCount).Item2);
                    schedule.ToBeInvigilatedBy(invigilator);
                    newCount += 1;
                }
            }
        }

        return schedules;
    }
    
    private static string GetCourseNumber(string courseCode)
    {
        return courseCode.Split(" ")[1];
    }
    private static void ApplyStyling(this ExcelRange range, int fontSize = 10, bool isBold = false, bool isMerge = false)
    {
        range.Style.WrapText = true;
        range.Style.Font.Name = "Arial";
        range.Style.Font.Size = fontSize;
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        range.Style.Font.Bold = isBold;
        range.Merge = isMerge;
    }

}

public enum ExamWorksheets
{
    General,
    Examiners,
    Invigilators,
    PracticalExams
}