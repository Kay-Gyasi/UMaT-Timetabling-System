using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Helpers;

public static class ExamsTimetableGenerator
{
    // TODO: Confirm if some rooms are for exams only (eg. Seminar room 1)
    // TODO: Use course examiners as invigilators (first, second)
    public static async Task<List<ExamsSchedule>> Generate(IEnumerable<Lecture> lectures, List<ClassRoom> rooms,
        List<SubClassGroup> subClassGroups, ExamsScheduleCommand command)
    {
        var examinableLectures = lectures.Where(x => x.Course!.IsExaminable).ToList();
        AppHelpers.Shuffle(examinableLectures);
        var schedules = new List<ExamsSchedule>();

        var examsGroupedByCourseNumber = GetSchedulesInInitialState(examinableLectures);
        var examPeriods = Enum.GetValues<ExamPeriod>();
        var examDates = new List<DateTime>();
        schedules = SetInitialDatesAndPeriods(command, examDates, examsGroupedByCourseNumber, examPeriods, schedules);
        schedules = AssignRooms(schedules, rooms, subClassGroups, examDates, examPeriods);
        
        // invigilators

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
                                       && x.SubClassGroupId == group.Id);
                if (scheduleIsCreated) continue;

                var courseCode = string.Join(" ", group.Name.Split(" ")[0],
                    GetCourseNumber(lecture.Course?.Code ?? string.Empty));
                var schedule = ExamsSchedule.Create(courseCode, group.Id)
                    .ToBeExaminedBy(lecture.LecturerId);
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
            var moments = examMoments.OrderBy(x => x.Item3.Count).ToList();
            AppHelpers.Shuffle(moments);
            (DateTime, ExamPeriod, List<int>) moment;
            while (classHasExamOnDate)
            {
                moment = moments[count];
                foreach (var exam in grouping)
                {
                    classHasExamOnDate =
                        schedules.Any(x => x.DateOfExam == moment.Item1 && x.SubClassGroupId == exam.SubClassGroupId);
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
                }

                count = 0;
                moment.Item3.AddRange(grouping.Select(x => x.SubClassGroupId));
            }
        }

        if (examsGroupedByCourseCode.Any(x => x.Any(a => a.ExamPeriod == null)))
        {
            throw new ExamDateAndPeriodNotSetException();
        }
        
        return schedules;
    }

    private static List<ExamsSchedule> AssignRooms(List<ExamsSchedule> schedules, IEnumerable<ClassRoom> rooms,
        List<SubClassGroup> subClassGroups, List<DateTime> examDates, ExamPeriod[] examPeriods)
    {
        var roomsForGeneralExams = rooms
            .Where(x => x.IsIncludedInGeneralAssignment)
            .OrderBy(x => x.Capacity)
            .ToList();
        
        foreach (var date in examDates)
        {
            foreach (var period in examPeriods)
            {
                var examsAtMoment = schedules
                    .Where(x => x.DateOfExam == date && x.ExamPeriod == period)
                    .ToList();

                foreach (var exam in examsAtMoment)
                {
                    var group = subClassGroups.First(x => x.Id == exam.SubClassGroupId);
                    var room = ClassRoom.Create("", 0);
                    var lastRoomsIndex = roomsForGeneralExams.Count - 1;
                    var hasCheckedAllRooms = false;
                    while (!hasCheckedAllRooms)
                    {
                        room = roomsForGeneralExams.FirstOrDefault(x => x.Capacity >= group?.Size);
                        room ??= roomsForGeneralExams[lastRoomsIndex];
                        
                        if (examsAtMoment.Any(x => x.RoomId == room.Id))
                        {
                            if (lastRoomsIndex == 0) break;
                            lastRoomsIndex -= 1;
                            continue;
                        };

                        hasCheckedAllRooms = true;
                    }
                    
                    // look for ones that have same examiner and place them in a single class
                    if (!hasCheckedAllRooms) throw new ExamNotAssignedRoomException();
                    exam.ToBeWrittenInRoom(room.Id);
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