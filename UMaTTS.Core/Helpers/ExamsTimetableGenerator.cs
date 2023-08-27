using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Helpers;

public static partial class ExamsTimetableGenerator
{
    public static List<List<ExamsSchedule>> Generate(List<ClassRoom> rooms, 
        List<SubClassGroup> subClassGroups, List<Lecturer> lecturers, List<IncomingCourse> courses, ExamsScheduleCommand command)
    {
        courses.Shuffle();
        courses = courses.OrderByDescending(x => x.Name!.Contains("drawing", StringComparison.OrdinalIgnoreCase)).ToList();
        var schedules = new List<ExamsSchedule>();
        var practicalSchedules = new List<ExamsSchedule>();

        var examsGroupedByCourseNumber = GetSchedulesInInitialState(courses, subClassGroups, lecturers);
        var examPeriods = Enum.GetValues<ExamPeriod>();
        var examDates = new List<DateTime>();
        var practicalDates = new List<DateTime>();
        
        (schedules, practicalSchedules) = SetInitialDatesAndPeriods(command, examDates, practicalDates,
                                            examPeriods, schedules, examsGroupedByCourseNumber, practicalSchedules);
        (schedules, practicalSchedules) = AssignRooms((schedules, practicalSchedules), rooms, 
                                            examDates, practicalDates, examPeriods);

        return AssignInvigilators((schedules, practicalSchedules), lecturers, courses);
    }
    
    private static (List<IGrouping<string, ExamsSchedule>> Exams, List<IGrouping<string, ExamsSchedule>> Practicals) 
        GetSchedulesInInitialState(List<IncomingCourse> courses, List<SubClassGroup> subClassGroups, List<Lecturer> lecturers)
    {
        var schedules = new List<ExamsSchedule>();
        var practicalSchedules = new List<ExamsSchedule>();

        ExtractSchedules(courses, subClassGroups, schedules, practicalSchedules, lecturers);

        var groupedExamSchedules = schedules.GroupBy(x => x.CourseNo)
                                    .OrderByDescending(a => a.Count())
                                    .ToList();
        var groupedPracticalSchedules = practicalSchedules.GroupBy(x => x.CourseNo)
                                        .OrderByDescending(a => a.Count())
                                        .ToList();
        return (groupedExamSchedules, groupedPracticalSchedules);
    }

    private static void ExtractSchedules(List<IncomingCourse> courses, List<SubClassGroup> subClassGroups,
        List<ExamsSchedule> schedules, List<ExamsSchedule> practicalSchedules, List<Lecturer> lecturers)
    {
        foreach (var course in courses)
        {
            if (course?.Code is null) continue;
            var groupsThatTakeCourse = subClassGroups.Where(x => x.Group.Name.StartsWith(course.ProgrammeCode ?? string.Empty) 
                                                                    && x.Group.Year == course.Year).ToList();
            foreach (var group in groupsThatTakeCourse)
            {
                var scheduleHasBeenCreated = schedules.Any(x =>
                                                x.CourseNo == GetCourseNumber(course?.Code ?? string.Empty)
                                                && x.SubClassGroups.Any(a => a.Id == group.Id));
                if (scheduleHasBeenCreated) continue;

                var courseCode = string.Join(AppHelpers.WhiteSpace, group.Name.Split(AppHelpers.WhiteSpace)[0],
                                    GetCourseNumber(course?.Code ?? string.Empty));

                var lecturer = lecturers.First(x => x.UmatId == course?.FirstExaminerStaffId);
                var schedule = ExamsSchedule.Create(courseCode)
                                .AddGroup(group)
                                .HasInfo(lecturer.Id, lecturer?.TitledName, course?.Name);

                schedules.Add(schedule);

                if (course!.HasPracticalExams)
                {
                    var practicalSchedule = ExamsSchedule.Create(courseCode)
                                .AddGroup(group)
                                .HasInfo(lecturer?.Id, lecturer?.TitledName, course?.Name);
                    practicalSchedules.Add(practicalSchedule);
                }
            }
        }
    }

    private static (List<ExamsSchedule>, List<ExamsSchedule>) SetInitialDatesAndPeriods(ExamsScheduleCommand command, 
        List<DateTime> examDates, List<DateTime> practicalDates, ExamPeriod[] examPeriods, List<ExamsSchedule> schedules,
        (List<IGrouping<string, ExamsSchedule>> Exams, List<IGrouping<string, ExamsSchedule>> Practicals) examsGroupedByCourseCode, 
        List<ExamsSchedule> practicalSchedules)
    {
        GenerateExamDates(examDates, command.StartDate, command.EndDate, command.IncludeSaturdays, command.IncludeSundays);
        GenerateExamDates(practicalDates, command.PracticalsStartDate, command.PracticalsEndDate, 
                            command.IncludeSaturdays, command.IncludeSundays);

        var (examMoments, practicalMoments) = GenerateExamMoments(examDates, practicalDates, examPeriods);

        SetDatesAndPeriodsForEachGrouping(examsGroupedByCourseCode.Exams, examMoments, schedules);
        SetDatesAndPeriodsForEachGrouping(examsGroupedByCourseCode.Practicals, practicalMoments, practicalSchedules);        
        return (schedules, practicalSchedules);
    }

    private static (List<ExamsSchedule>, List<ExamsSchedule>) AssignRooms((List<ExamsSchedule> Exams, 
        List<ExamsSchedule> Practicals) schedules, IEnumerable<ClassRoom> rooms, 
        List<DateTime> examDates, List<DateTime> practicalDates, ExamPeriod[] examPeriods)
    {
        // TODO: Work on preferred rooms for practical exams
        var roomsForGeneralExams = rooms.Where(x => x.IncludeInGeneralAssignment)
                                    .OrderBy(x => x.Capacity)
                                    .ThenBy(x => x.Name.Contains("auditorium", StringComparison.OrdinalIgnoreCase))
                                    .ToList();

        // Cl 2

        AssignRooms(examDates, schedules.Exams, examPeriods, roomsForGeneralExams);
        AssignRooms(practicalDates, schedules.Practicals, examPeriods, roomsForGeneralExams);
        return schedules;
    }

    private static void AssignRooms(List<DateTime> examDates, List<ExamsSchedule> schedules, ExamPeriod[] examPeriods,
    List<ClassRoom> rooms)
    {
        foreach (var date in examDates)
        {
            foreach (var period in examPeriods)
            {
                var examsAtMoment = schedules
                                    .Where(x => x.DateOfExam == date && x.ExamPeriod == period)
                                    .ToList();

                while (examsAtMoment.Count > rooms.Count)
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
                    var groupSize = exam.SubClassGroups.Sum(x => x.Size);
                    var room = ClassRoom.Create("", 0);
                    var lastRoomsIndex = rooms.Count - 1;
                    var hasCheckedAllRooms = false;
                    while (!hasCheckedAllRooms)
                    {
                        room = rooms.FirstOrDefault(x => x.Capacity >= groupSize && !assignedRooms.Contains(x.Id));
                        room ??= rooms[lastRoomsIndex];

                        if (examsAtMoment.Any(x => x.RoomId == room.Id))
                        {
                            if (lastRoomsIndex == 0) break;
                            lastRoomsIndex -= 1;
                            continue;
                        }

                        hasCheckedAllRooms = true;
                    }

                    if (!hasCheckedAllRooms) throw new ExamNotAssignedRoomException();
                    exam.ToBeWrittenInRoom(room.Id);
                    assignedRooms.Add(room.Id);
                }
            }
        }
    }

    private static List<List<ExamsSchedule>> AssignInvigilators((List<ExamsSchedule> Exams, List<ExamsSchedule> Practicals) schedules,
        List<Lecturer> lecturers, List<IncomingCourse> courses)
    {
        lecturers.Shuffle();
        var examsAndPracticalSchedules = new List<List<ExamsSchedule>> { schedules.Exams, schedules.Practicals };

        foreach (var schdules in examsAndPracticalSchedules)
        {
            var groupedSchedules = schdules.GroupBy(x => x.CourseNo).ToList();
            foreach (var groupedSchedule in groupedSchedules)
            {
                if (groupedSchedule is null) continue;
                var invigilators = new HashSet<(string Name, int Id, string Course)>();

                foreach (var exam in groupedSchedule)
                {
                    var coursesForExams = GetCoursesForExam(courses, exam);
                    if (coursesForExams is null) continue;

                    GetExaminersForEachCourseAndMakeInvigilators(coursesForExams, lecturers, invigilators);
                }

                var numberOfInvigilatorsForExam = groupedSchedule.Sum(x => x.CourseCodes?.Count);
                foreach (var schedule in groupedSchedule)
                {
                    foreach (var subClassSize in schedule.SubClassGroups.Select(x => x.Size))
                    {
                        var result = subClassSize;
                        var count = 0;
                        while (result > 0)
                        {
                            result -= 80;
                            count += 1;
                        }

                        numberOfInvigilatorsForExam += count - 1;
                    }
                }

                GetMakeUpInvigilators(invigilators, lecturers, numberOfInvigilatorsForExam, count: 0);
                AssignInvigilators(groupedSchedule, lecturers, invigilators, count: 0);
            }
        }

        return examsAndPracticalSchedules;
    }

    private static void AssignInvigilators(IGrouping<string, ExamsSchedule> groupedSchedule,
        List<Lecturer> lecturers, HashSet<(string Name, int Id, string Course)> invigilators, int count)
    {
        foreach (var schedule in groupedSchedule)
        {
            if (schedule.CourseCodes is null) continue;
            var size = schedule.SubClassGroups.Sum(x => x.Size);
            while (size > 0)
            {
                size -= 80;
                var invigilator = lecturers.First(x => x.Id == invigilators.ElementAt(count).Id);
                schedule.ToBeInvigilatedBy(invigilator);
                count += 1;
            }            
        }
    }

    private static void GetExaminersForEachCourseAndMakeInvigilators(List<IncomingCourse> coursesForExams,
        List<Lecturer> lecturers, HashSet<(string, int, string Course)> invigilators)
    {
        foreach (var course in coursesForExams)
        {
            var firstExaminer = lecturers.FirstOrDefault(x => x.UmatId == course.FirstExaminerStaffId);
            if (firstExaminer?.Name is null) continue;
            invigilators.Add((firstExaminer.Name, firstExaminer.Id, course.Name ?? string.Empty));

            var secondExaminer = lecturers.FirstOrDefault(x => x.UmatId == course.SecondExaminerStaffId);
            if (secondExaminer?.Name is null) continue;
            invigilators.Add((secondExaminer.Name, secondExaminer.Id, course.Name ?? string.Empty));
        }
    }

    private static void GetMakeUpInvigilators(HashSet<(string Name, int Id, string Course)> invigilators, List<Lecturer> lecturers,
        int? numberOfInvigilatorsForExam, int count)
    {
        while (invigilators.Count < numberOfInvigilatorsForExam)
        {
            var selected = lecturers[count % lecturers.Count];
            if (selected is null) continue;

            var isProfessor = selected.TitledName!.Contains("prof", StringComparison.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(selected.Name) || isProfessor || invigilators.Any(x => x.Name == selected.Name))
            {
                count += 1;
                continue;
            }

            invigilators.Add((selected.Name, selected.Id, string.Empty));
            count += 1;
        }
    }

    private static List<IncomingCourse>? GetCoursesForExam(List<IncomingCourse> courses, ExamsSchedule exam)
    {
        var coursesForExams = new List<IncomingCourse>();
        if (exam.CourseCodes is null) return null;

        foreach (var courseCode in exam.CourseCodes.Distinct())
        {
            var course = courses.FirstOrDefault(x => x.Code == courseCode);
            course ??= courses.FirstOrDefault(x =>
                        GetCourseNumber(x.Code ?? "") == GetCourseNumber(courseCode)
                        && x.ProgrammeCode == courseCode?.Split(AppHelpers.WhiteSpace)[0]);
            if (course is null) continue;
            coursesForExams.Add(course);
        }

        return coursesForExams;
    }

    private static void GenerateExamDates(List<DateTime> examDates, DateTime startDate, DateTime endDate, 
        bool includeSaturdays, bool includeSundays)
    {
        var date = startDate;
        while (date <= endDate)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday when !includeSaturdays:
                    date = date.AddDays(1);
                    continue;
                case DayOfWeek.Sunday when !includeSundays:
                    date = date.AddDays(1);
                    continue;
            }

            examDates.Add(date);
            date = date.AddDays(1);
            if (date.DayOfWeek == DayOfWeek.Saturday && !includeSaturdays)
            {
                date = date.AddDays(1);
            }

            if (date.DayOfWeek == DayOfWeek.Sunday && !includeSundays)
            {
                date = date.AddDays(1);
            }
        }
    }

    private static (List<(DateTime, ExamPeriod, List<int>)>, List<(DateTime, ExamPeriod, List<int>)>) 
        GenerateExamMoments(List<DateTime> examDates, List<DateTime> practicalDates, ExamPeriod[] examPeriods)
    {
        var examMoments = new List<(DateTime, ExamPeriod, List<int>)>();
        foreach (var examDate in examDates)
        {
            foreach (var period in examPeriods)
            {
                examMoments.Add((examDate, period, new List<int>()));
            }
        }

        var practicalMoments = new List<(DateTime, ExamPeriod, List<int>)>();
        foreach (var pracDate in practicalDates)
        {
            foreach (var period in examPeriods)
            {
                practicalMoments.Add((pracDate, period, new List<int>()));
            }
        }

        return (examMoments, practicalMoments);
    }

    private static void SetDatesAndPeriodsForEachGrouping(List<IGrouping<string, ExamsSchedule>> examsGroupedByCourseCode,
        List<(DateTime Date, ExamPeriod Period, List<int> AssignedGroups)> examMoments, List<ExamsSchedule> schedules)
    {
        examsGroupedByCourseCode.Shuffle();
        foreach (var grouping in examsGroupedByCourseCode)
        {
            var classHasExamOnDate = true;
            var count = 0;
            var moments = examMoments;
            moments.Shuffle();
            moments = moments.OrderBy(x => x.Period == ExamPeriod.Evening).ToList(); // an attempt to get low evening assignments

            if (grouping.Any(x => x.CourseName!.Contains("drawing", StringComparison.OrdinalIgnoreCase)))
            {
                moments = moments.OrderByDescending(x => x.Date.DayOfWeek == DayOfWeek.Saturday && x.Period != ExamPeriod.Evening)
                                    .ToList();
            }

            (DateTime Date, ExamPeriod Period, List<int> AssignedGroups) moment;
            while (classHasExamOnDate)
            {
                moments = moments.OrderBy(x => x.AssignedGroups.Count).ToList();
                moment = moments[count];
                foreach (var exam in grouping)
                {
                    foreach (var group in exam.SubClassGroups)
                    {
                        classHasExamOnDate = schedules.Any(x => x.DateOfExam == moment.Date
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
                    exam.OnPeriod(moment.Period)
                        .ToBeWrittenOn(moment.Date);
                    schedules.Add(exam);

                    count = 0;
                    moment.AssignedGroups.AddRange(exam.SubClassGroups.Select(x => x.Id));
                }
            }

            if (grouping.Any(x => x.ExamPeriod == null))
            {
                throw new ExamDateAndPeriodNotSetException();
            }
        }
    }
}

public enum ExamWorksheets
{
    General,
    Courses,
    Examiners,
    Invigilators,
    Practicals
}