using LinqKit;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Helpers
{
    public static partial class TimetableGenerator
    {
        public static (List<LectureSchedule>, List<OnlineLectureSchedule>) Generate(List<LectureSchedule> schedules, 
            List<OnlineLectureSchedule> onlineSchedules, List<Lecture> lectures)
        {
            ResetSchedules(schedules, onlineSchedules);
            AppHelpers.Shuffle(schedules);
            AppHelpers.Shuffle(lectures);

            AssignLecturesToSchedules(schedules, onlineSchedules, lectures);
            return (schedules, onlineSchedules);
        }

        private static void AssignLecturesToSchedules(List<LectureSchedule> schedules, 
            List<OnlineLectureSchedule> onlineSchedules, List<Lecture> lectures)
        {
            lectures = lectures
                        .OrderByDescending(x => x.PreferredRoom is not null)
                        .ToList();

            foreach (var lecture in lectures)
            {
                var builder = PredicateBuilder.New<LectureSchedule>(x => true);
                var onlineBuilder = PredicateBuilder.New<OnlineLectureSchedule>(x => true);
                bool isScheduled = false;

                BuildBasePredicateForSchedulingLecture(builder, onlineBuilder, schedules, onlineSchedules, lecture);

                isScheduled = ScheduleVleLecture(onlineSchedules, onlineBuilder, lecture);
                if (isScheduled) continue;

                var eligibleSchedules = schedules.Where(builder)
                                            .OrderBy(x => x.Room.Capacity)
                                            .ToList();

                isScheduled = ScheduleTwoPeriodLecture(eligibleSchedules, lecture);
                if (isScheduled) continue;

                ScheduleOnePeriodLecture(eligibleSchedules, lecture);
            }
        }

        private static bool ScheduleTwoPeriodLecture(List<LectureSchedule> eligibleSchedules, Lecture lecture)
        {
            if (lecture.Duration != 2) return false;

            var schedule = eligibleSchedules.FirstOrDefault(x => x.FirstLectureId == null
                                && x.SecondLectureId == null
                                && x.Room.Name == lecture.PreferredRoom);

            schedule ??= eligibleSchedules.FirstOrDefault(x => x.FirstLectureId == null
                            && x.SecondLectureId == null
                            && x.Room.Capacity >= lecture.SubClassGroups.Sum(s => s.Size));

            schedule ??= eligibleSchedules.LastOrDefault(x => x.FirstLectureId == null && x.SecondLectureId == null);
            schedule?.HasLecture(lecture.Id, lecture.Id);
            return true;
        }

        private static void ScheduleOnePeriodLecture(List<LectureSchedule> eligibleSchedules, Lecture lecture)
        {
            var eligibleSchedulesForOnePeriodLectures = eligibleSchedules.Where(x =>
                                                            x.FirstLectureId == null ||
                                                            x.SecondLectureId == null).ToList();            
            if (!eligibleSchedulesForOnePeriodLectures.Any()) return;

            var schedule = eligibleSchedulesForOnePeriodLectures.FirstOrDefault(x => x.Room.Name == lecture.PreferredRoom);
            schedule ??= eligibleSchedulesForOnePeriodLectures.FirstOrDefault(x => x.Room.Capacity >= lecture.SubClassGroups.Sum(s => s.Size));
            schedule ??= eligibleSchedulesForOnePeriodLectures.LastOrDefault();

            if (schedule?.FirstLectureId is null)
            {
                schedule?.HasLecture(lecture.Id, null);
                return;
            }

            if (schedule.SecondLectureId is null) schedule.HasLecture(null, lecture.Id);
        }

        private static bool ScheduleVleLecture(List<OnlineLectureSchedule> onlineSchedules,
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, Lecture lecture)
        {
            if (!lecture.IsVLE) return false;

            var eligibleOnlineSchedules = onlineSchedules.Where(onlineBuilder).ToList();
            var rand = new Random();
            var onlineSchedule = eligibleOnlineSchedules[rand.Next(eligibleOnlineSchedules.Count)];
            onlineSchedule?.AddLecture(lecture);
            return true;
        }

        private static void BuildBasePredicateForSchedulingLecture(ExpressionStarter<LectureSchedule> builder, 
            ExpressionStarter<OnlineLectureSchedule> onlineBuilder, List<LectureSchedule> schedules, 
            List<OnlineLectureSchedule> onlineSchedules, Lecture lecture)
        {
            for (var i = 0; i < 5; i++)
            {
                builder.And(x => x.Room.IncludeInGeneralAssignment);
                builder.Or(x => x.Room.Name == lecture.PreferredRoom);

                CheckIfAnySubClassHasSameLectureToday(builder, schedules, lecture, day: i);

                var numOfLecturesForLecturer = GetNumberOfLecturesForLecturerToday(schedules, onlineSchedules, lecture, day: i);
                if (numOfLecturesForLecturer <= 4) continue;

                var i1 = i;
                if (lecture.IsVLE)
                {
                    onlineBuilder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
                    continue;
                }

                builder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
            }
        }

        private static int GetNumberOfLecturesForLecturerToday(List<LectureSchedule> schedules,
            List<OnlineLectureSchedule> onlineSchedules, Lecture lecture, int day)
        {
            var schedulesForLecturerPerDay = schedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(day)
                                                    && x.FirstLecture?.LecturerId == lecture.LecturerId
                                                    || x.SecondLecture?.LecturerId == lecture.LecturerId);

            var onlineSchedulesForLecturerPerDay = onlineSchedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(day)
                                                    && x.Lectures.Any(a => a.LecturerId == lecture.LecturerId));

            return schedulesForLecturerPerDay + onlineSchedulesForLecturerPerDay;
        }

        private static void CheckIfAnySubClassHasSameLectureToday(ExpressionStarter<LectureSchedule> builder, 
            List<LectureSchedule> schedules, Lecture lecture, int day)
        {
            foreach (var sub in lecture.SubClassGroups)
            {
                var schedulesForDay = schedules.Where(x => x.DayOfWeek == AppHelper.GetDayOfWeek(day));
                var subHasSameLectureOnDay = schedulesForDay.Any(x =>
                                                (x.FirstLecture != null && x.FirstLecture.Course?.Name == lecture.Course?.Name 
                                                && x.FirstLecture.SubClassGroups.Contains(sub))
                                                || (x.SecondLecture != null && x.SecondLecture.Course?.Name == lecture.Course?.Name 
                                                && x.SecondLecture.SubClassGroups.Contains(sub)));

                if (subHasSameLectureOnDay)
                {
                    builder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(day));
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