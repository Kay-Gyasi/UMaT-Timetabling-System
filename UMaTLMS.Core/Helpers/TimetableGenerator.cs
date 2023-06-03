using LinqKit;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Helpers
{
    public static class TimetableGenerator
    {
        public static List<LectureSchedule> Generate(List<LectureSchedule> schedules, List<Lecture> lectures)
        {

            Shuffle(schedules);
            foreach (var lecture in lectures)
            {
                var builder = PredicateBuilder.New<LectureSchedule>(x => true);
            
                for (var i = 0; i < 5; i++)
                {
                    var numOfLecturesForLecturer = schedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(i) 
                                                                        && x.Lecture?.LecturerId != null 
                                                                        && x.Lecture.LecturerId == lecture.LecturerId);
                    if (numOfLecturesForLecturer < 4) continue;

                    var i1 = i;
                    builder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
                }
            
                var eligibleSchedules = schedules.Where(builder);
                var schedule = eligibleSchedules.FirstOrDefault(x => x.LectureId == null);
                schedule?.HasLecture(lecture.Id);
            }

            return schedules;
        }
        
        private static void Shuffle<T>(List<T> list)
        {
            var random = new Random();

            for (var i = list.Count - 1; i > 0; i--)
            {
                var j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}