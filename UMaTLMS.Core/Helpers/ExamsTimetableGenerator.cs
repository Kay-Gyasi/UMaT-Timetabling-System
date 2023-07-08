using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Helpers;

public static class ExamsTimetableGenerator
{
    // TODO:
    // Build exams timetable file
    // Work on UI
    // Learn Node.js
    public static List<ExamsSchedule> Generate(IEnumerable<Lecture> lectures, List<ClassRoom> rooms,
        List<SubClassGroup> subClassGroups, List<Lecturer> lecturers, List<IncomingCourse> courses, ExamsScheduleCommand command)
    {
        var examinableLectures = lectures.Where(x => x.Course!.IsExaminable).ToList();
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
    
    public static async Task<bool> GenerateTimetable(List<ExamsSchedule> schedules)
    {
        // build excel file
        await Task.CompletedTask;
        return true;
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
                    .ToBeExaminedBy(lecture.LecturerId, lecture.Lecturer?.Name);
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
            .Where(x => x.IsIncludedInGeneralAssignment)
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
}