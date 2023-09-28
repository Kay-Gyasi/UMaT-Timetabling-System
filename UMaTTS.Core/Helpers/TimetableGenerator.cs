using LinqKit;
using System.Text.Json;
using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Helpers
{
    public static partial class TimetableGenerator
    {
        public static void Generate(TimetableGenerationCommand command)
        {
            ResetSchedules(command.Schedules, command.OnlineSchedules);
            command.Lectures.Shuffle();
            command.Schedules.Shuffle(seed: 50);            

            AssignLecturesToSchedules(command);
        }

        private static void AssignLecturesToSchedules(TimetableGenerationCommand command)
        {
            AddPreferredRoomsFromPreferences(command.Lectures, command.Preferences);
            command.Lectures = command.Lectures
                        .OrderByDescending(x => x.PreferredRoom is not null)
                        .ThenByDescending(x => x.Duration)
                        .ToList();

            foreach (var lecture in command.Lectures)
            {
                var builder = PredicateBuilder.New<LectureSchedule>(x => true);
                var onlineBuilder = PredicateBuilder.New<OnlineLectureSchedule>(x => true);
                bool isScheduled = false;

                BuildBasePredicateForSchedulingLecture(builder, onlineBuilder, command, lecture);

                isScheduled = ScheduleVleLecture(command.OnlineSchedules, command.Schedules, onlineBuilder, lecture);
                if (isScheduled) continue;

                var eligibleSchedules = command.Schedules.Where(builder)
                                            .OrderBy(x => x.Room.Capacity)
                                            .ToList();
                if (!eligibleSchedules.Any())
                {
                    builder = PredicateBuilder.New<LectureSchedule>(x => true);
                    onlineBuilder = PredicateBuilder.New<OnlineLectureSchedule>(x => true);
                    builder.And(x => x.Room.IncludeInGeneralAssignment);
                    builder.Or(x => x.Room.Name == lecture.PreferredRoom);

                    eligibleSchedules = command.Schedules.Where(builder)
                                            .OrderBy(x => x.Room.Capacity)
                                            .ToList();
                }

                isScheduled = ScheduleTwoPeriodLecture(eligibleSchedules, command.OnlineSchedules, lecture);
                if (isScheduled) continue;

                ScheduleOnePeriodLecture(eligibleSchedules, command.OnlineSchedules, lecture);
            }
        }

        private static bool ScheduleTwoPeriodLecture(List<LectureSchedule> eligibleSchedules, List<OnlineLectureSchedule> onlineSchedules,
            Lecture lecture)
        {
            if (lecture.Duration != 2) return false;
            var invalidScheduleIds = new List<int>();

            while (true)
            {
                eligibleSchedules = eligibleSchedules.Where(x => invalidScheduleIds.Contains(x.Id) == false).ToList();
                var schedule = eligibleSchedules.FirstOrDefault(x => x.FirstLectureId == null
                                && x.SecondLectureId == null
                                && x.Room.Name == lecture.PreferredRoom);

                schedule ??= eligibleSchedules.FirstOrDefault(x => x.FirstLectureId == null
                                && x.SecondLectureId == null
                                && x.Room.Capacity >= lecture.SubClassGroups.Sum(s => s.Size));

                schedule ??= eligibleSchedules.LastOrDefault(x => x.FirstLectureId == null 
                                                                    && x.SecondLectureId == null);

                bool anySubHasVleLectureAtTime = false;
                foreach (var sub in lecture.SubClassGroups)
                {
                    if (schedule is null) break;
                    anySubHasVleLectureAtTime = onlineSchedules.Any((x => x.TimePeriod == schedule.TimePeriod
                                                    && x.DayOfWeek!.Value == schedule.DayOfWeek!.Value
                                                    && (x.Lectures.Any(a => a.SubClassGroups.Contains(sub)))));
                    if (anySubHasVleLectureAtTime)
                    {
                        invalidScheduleIds.Add(schedule.Id); 
                        break;
                    }
                }

                if (anySubHasVleLectureAtTime) continue;
                schedule?.HasFirstLecture(lecture);
                schedule?.HasSecondLecture(lecture);
                return true;
            }
        }

        private static void ScheduleOnePeriodLecture(List<LectureSchedule> eligibleSchedules, List<OnlineLectureSchedule> onlineSchedules, 
            Lecture lecture)
        {
            var invalidScheduleIds = new List<int>();
            
            while (true)
            {
                eligibleSchedules = eligibleSchedules.Where(x => invalidScheduleIds.Contains(x.Id) == false).ToList();
                var eligibleSchedulesForOnePeriodLectures = eligibleSchedules.Where(x =>
                                                            x.FirstLectureId == null ||
                                                            x.SecondLectureId == null).ToList();
                if (!eligibleSchedulesForOnePeriodLectures.Any()) return;

                var schedule = eligibleSchedulesForOnePeriodLectures.FirstOrDefault(x => x.Room.Name == lecture.PreferredRoom);
                schedule ??= eligibleSchedulesForOnePeriodLectures.FirstOrDefault(x => x.Room.Capacity >= lecture.SubClassGroups.Sum(s => s.Size));
                schedule ??= eligibleSchedulesForOnePeriodLectures.LastOrDefault();

                bool anySubHasVleLectureAtTime = false;
                foreach (var sub in lecture.SubClassGroups)
                {
                    if (schedule is null) break;
                    anySubHasVleLectureAtTime = onlineSchedules.Any((x => x.TimePeriod == schedule.TimePeriod
                                                    && x.DayOfWeek!.Value == schedule.DayOfWeek!.Value
                                                    && (x.Lectures.Any(a => a.SubClassGroups.Contains(sub)))));
                    if (anySubHasVleLectureAtTime)
                    {
                        invalidScheduleIds.Add(schedule.Id);
                        break;
                    }
                }

                if (anySubHasVleLectureAtTime) continue;
                if (schedule?.FirstLectureId is null)
                {
                    schedule?.HasFirstLecture(lecture);
                    return;
                }

                if (schedule.SecondLectureId is null) schedule.HasSecondLecture(lecture);
            }
        }

        private static bool ScheduleVleLecture(List<OnlineLectureSchedule> onlineSchedules, List<LectureSchedule> schedules,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, Lecture lecture)
        {
            if (!lecture.IsVLE) return false;

            var eligibleOnlineSchedules = onlineSchedules.Where(onlineBuilder).ToList();
            if (!eligibleOnlineSchedules.Any()) 
                throw new InvalidDataException();
            var rand = new Random();

            while (true)
            {
                var onlineSchedule = eligibleOnlineSchedules[rand.Next(eligibleOnlineSchedules.Count)];
                bool anySubHasLectureAtTime = false;
                foreach (var sub in lecture.SubClassGroups)
                {
                    var practicalSchedulesAtSameTime = schedules.Where(x => x.TimePeriod == onlineSchedule.TimePeriod 
                                                && x.DayOfWeek!.Value == onlineSchedule.DayOfWeek!.Value).ToList();
                    anySubHasLectureAtTime = practicalSchedulesAtSameTime.Any(x => 
                                                    (x.FirstLecture != null && x.FirstLecture.SubClassGroups.Contains(sub))
                                                    || (x.SecondLecture != null && x.SecondLecture.SubClassGroups.Contains(sub)));
                    if (anySubHasLectureAtTime) 
                        break;
                }

                if (anySubHasLectureAtTime) continue;
                onlineSchedule?.AddLecture(lecture);
                return true;
            }
        }

        private static void BuildBasePredicateForSchedulingLecture(ExpressionStarter<LectureSchedule> builder, 
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, TimetableGenerationCommand command, Lecture lecture)
        {
            AddPreferencesToBuilder(builder, onlineBuilder, lecture, command.Preferences);
            AddConstraintsToBuilder(builder, onlineBuilder, command, lecture);
        }

        private static void AddConstraintsToBuilder(ExpressionStarter<LectureSchedule> builder,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, TimetableGenerationCommand command, Lecture lecture)
        {
            builder.And(x => x.Room.IncludeInGeneralAssignment);
            builder.Or(x => x.Room.Name == lecture.PreferredRoom);

            foreach (var day in command.DaysOfWeekForLectures)
            {
                CheckIfAnySubClassHasSameLectureToday(builder, onlineBuilder, command, lecture, day);
                CheckNumOfLecturesForLecturerForDay(builder, onlineBuilder, command, lecture, day);

                var numOfLecturesForClassToday = GetNumberOfLecturesForClassToday(command, lecture, day);
                if (numOfLecturesForClassToday >= 4)
                {
                    if (lecture.IsVLE)
                    {
                        onlineBuilder.And(x => x.DayOfWeek!.Value != day);
                        return;
                    }

                    builder.And(x => x.DayOfWeek!.Value != day);
                }
            }
        }

        private static void CheckNumOfLecturesForLecturerForDay(ExpressionStarter<LectureSchedule> builder,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, TimetableGenerationCommand command, Lecture lecture, DayOfWeek day)
        {
            var numOfLecturesForLecturerToday = GetNumberOfLecturesForLecturerToday(command.Schedules, command.OnlineSchedules, lecture, day);
            var maxNumberOfLecturesPerDayForLecturer = command.Constraints.FirstOrDefault(x =>
                                                            x.Type == ConstraintType.MaxLecturesPerDayForLecturer
                                                            && x.LecturerId == lecture.LecturerId)?.Value;
            maxNumberOfLecturesPerDayForLecturer ??= command.Constraints.SingleOrDefault(x =>
                                                        x.Type == ConstraintType.GeneralMaxLecturesPerDay)?.Value;
            maxNumberOfLecturesPerDayForLecturer ??= "4";
            if (numOfLecturesForLecturerToday >= Convert.ToInt32(maxNumberOfLecturesPerDayForLecturer))
            {
                if (lecture.IsVLE)
                {
                    onlineBuilder.And(x => x.DayOfWeek != day);
                    return;
                }

                builder.And(x => x.DayOfWeek != day);
            }
        }

        private static void AddPreferencesToBuilder(ExpressionStarter<LectureSchedule> builder,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, Lecture lecture, List<Preference> preferences)
        {
            var requiredPreferences = preferences.Where(x => x.LecturerId == lecture.LecturerId
                                                                || (x.Course != null && x.Course.Name == lecture.Course!.Name)).ToList();

            // TODO Refactor
            foreach (var preference in requiredPreferences)
            {
                switch (preference.Type)
                {
                    case PreferenceType.DayNotAvailable:
                        var value = JsonSerializer.Deserialize<DayNotAvailable>(preference.Value);
                        if (value is null) continue;
                        if (lecture.IsVLE) onlineBuilder.And(x => x.DayOfWeek!.Value != value.Day);
                        else builder.And(x => x.DayOfWeek!.Value != value.Day);
                        continue;
                    case PreferenceType.TimeNotAvailable:
                        var value1 = JsonSerializer.Deserialize<TimeNotAvailable>(preference.Value);
                        if (value1 is null) continue;
                        if (value1.Day is not null)
                        {
                            if (lecture.IsVLE) onlineBuilder.And(x => x.DayOfWeek!.Value == value1.Day ? 
                                                                        value1.Time!.Contains(x.TimePeriod) == false : true);
                            else builder.And(x => x.DayOfWeek!.Value == value1.Day ? value1.Time!.Contains(x.TimePeriod) == false : true);
                            continue;
                        }

                        if (lecture.IsVLE) onlineBuilder.And(x => value1.Time!.Contains(x.TimePeriod) == false);
                        else builder.And(x => value1.Time!.Contains(x.TimePeriod) == false);
                        continue;
                    case PreferenceType.PreferredDayOfWeek:
                        var value2 = JsonSerializer.Deserialize<DayNotAvailable>(preference.Value);
                        if (value2 is null) continue;
                        if (lecture.IsVLE) onlineBuilder.And(x => x.DayOfWeek!.Value == value2.Day);
                        else builder.And(x => x.DayOfWeek!.Value == value2.Day);
                        continue;
                    case PreferenceType.PreferredLectureRoom:
                        continue;
                    default:
                        continue;
                }
            }
        }

        private static void AddPreferredRoomsFromPreferences(List<Lecture> lectures, List<Preference> preferences)
        {
            foreach (var lecture in lectures)
            {
                if (lecture.IsVLE) continue;
                var requiredPreferences = preferences.Where(x => x.LecturerId == lecture.LecturerId 
                                            || (x.Course != null && x.Course.Name == lecture.Course!.Name)).ToList();

                foreach (var preference in requiredPreferences)
                {
                    var value = JsonSerializer.Deserialize<PreferredLectureRoom>(preference.Value);
                    if (value is null) continue;

                    if (string.IsNullOrWhiteSpace(lecture.PreferredRoom))
                    {
                        lecture.HasPreferredRoom(value.Room);
                    }
                }
            }
        }

        private static int GetNumberOfLecturesForLecturerToday(List<LectureSchedule> schedules,
            List<OnlineLectureSchedule> onlineSchedules, Lecture lecture, DayOfWeek day)
        {
            var schedulesForLecturerToday = schedules.Count(x => x.DayOfWeek != null && x.DayOfWeek == day
                                                    && (x.FirstLecture?.LecturerId == lecture.LecturerId
                                                    || x.SecondLecture?.LecturerId == lecture.LecturerId));

            var onlineSchedulesForLecturerToday = onlineSchedules.Count(x => x.DayOfWeek != null && x.DayOfWeek == day
                                                    && x.Lectures.Any(a => a.LecturerId == lecture.LecturerId));

            return schedulesForLecturerToday + onlineSchedulesForLecturerToday;
        }

        private static int GetNumberOfLecturesForClassToday(TimetableGenerationCommand command, Lecture lecture, DayOfWeek day)
        {
            int schedulesForClassToday = 0;
            int onlineSchedulesForClassToday = 0;
            foreach (var sub in lecture.SubClassGroups)
            {
                var schedulesForToday = command.Schedules.Where(x => x.DayOfWeek != null && x.DayOfWeek == day);
                var first = schedulesForToday.Count(x => x.FirstLecture != null && x.FirstLecture.SubClassGroups.Contains(sub));
                var second = schedulesForToday.Count(x => x.SecondLecture != null && x.SecondLecture.SubClassGroups.Contains(sub));
                schedulesForClassToday = first + second;

                onlineSchedulesForClassToday = command.OnlineSchedules.Count(x => x.DayOfWeek != null && x.DayOfWeek == day
                                                    && x.Lectures.Any(a => a.SubClassGroups.Contains(sub)));
                if (schedulesForClassToday + onlineSchedulesForClassToday >= 4) 
                    break;
            }

            return schedulesForClassToday + onlineSchedulesForClassToday;
        }

        private static void CheckIfAnySubClassHasSameLectureToday(ExpressionStarter<LectureSchedule> builder,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, TimetableGenerationCommand command, Lecture lecture, DayOfWeek day)
        {
            foreach (var sub in lecture.SubClassGroups)
            {
                var schedulesForDay = command.Schedules.Where(x => x.DayOfWeek != null && x.DayOfWeek == day);
                var first = schedulesForDay.Any(x => x.FirstLecture?.Course != null
                                                    && x.FirstLecture.Course.Name == lecture.Course!.Name
                                                    && x.FirstLecture.SubClassGroups.Contains(sub));
                var second = schedulesForDay.Any(x => x.SecondLecture?.Course != null
                                                    && x.SecondLecture.Course.Name == lecture.Course!.Name
                                                    && x.SecondLecture.SubClassGroups.Contains(sub));
                var subHasSameLectureOnDay = first || second;

                var onlineSchedulesForDay = command.OnlineSchedules.Where(x => x.DayOfWeek != null && x.DayOfWeek == day);
                var subHasSameVleLectureOnDay = onlineSchedulesForDay.Any(x => x.Lectures.Any(x => x.Course != null 
                                                                            && x.Course.Name == lecture.Course!.Name
                                                                            && x.SubClassGroups.Contains(sub)));

                if (subHasSameLectureOnDay || subHasSameVleLectureOnDay)
                {
                    builder.And(x => x.DayOfWeek != day);
                    onlineBuilder.And(x => x.DayOfWeek != day);
                    break;
                }
            }
        }


        private static void ResetSchedules(List<LectureSchedule> schedules, List<OnlineLectureSchedule> onlineSchedules)
        {
            foreach (var s in schedules.Where(x => x.FirstLecture is not null || x.SecondLecture is not null))
            {
                s.Reset();
            }

            foreach (var o in onlineSchedules.Where(x => x.Lectures.Any()))
            {
                o.Reset();
            }
        }
    }
}