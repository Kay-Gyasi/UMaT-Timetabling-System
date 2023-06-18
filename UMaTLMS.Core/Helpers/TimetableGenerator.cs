using LinqKit;
using OfficeOpenXml;
using UMaTLMS.Core.Entities;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Helpers
{
    public static class TimetableGenerator
    {
        public static (List<LectureSchedule>, List<OnlineLectureSchedule>) Generate(List<LectureSchedule> schedules, List<OnlineLectureSchedule> onlineSchedules, List<Lecture> lectures)
        {

            Shuffle(lectures);
            foreach (var lecture in lectures)
            {
                var builder = PredicateBuilder.New<LectureSchedule>(x => true);
                var onlineBuilder = PredicateBuilder.New<OnlineLectureSchedule>(x => true);
            
                for (var i = 0; i < 5; i++)
                {
                    var numOfLecturesForLecturer = schedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(i) 
                                                                        && x.Lecture?.LecturerId != null 
                                                                        && x.Lecture.LecturerId == lecture.LecturerId) 
                        + onlineSchedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(i) && x.Lectures.Any(a => a.LecturerId == lecture.LecturerId));
                    if (numOfLecturesForLecturer < 4) continue;

                    var i1 = i;
                    if (lecture.IsVLE)
                    {
                        onlineBuilder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
                        continue;
                    }

                    builder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
                }

                if (lecture.IsVLE)
                {
                    var eligibleOnlineSchedules = onlineSchedules.Where(onlineBuilder).ToList();
                    var rand = new Random();
                    var onlineSchedule = eligibleOnlineSchedules[rand.Next(eligibleOnlineSchedules.Count)];
                    onlineSchedule?.AddLecture(lecture);
                    continue;
                }
            
                var eligibleSchedules = schedules.Where(builder);
                var schedule = eligibleSchedules.FirstOrDefault(x => x.LectureId == null);
                schedule?.HasLecture(lecture.Id);
            }

            return (schedules, onlineSchedules);
        }

        public static async Task GetTimetable(IExcelReader excelReader ,IEnumerable<LectureSchedule> lectureSchedules, IEnumerable<OnlineLectureSchedule> onlineLectureSchedules)
        {
            // Create a new Excel package
            using var excelPackage = excelReader.CreateNew("_content/Timetable.xlsx");

            // Group the lecture schedules by day of week
            var groupedLectureSchedules = lectureSchedules.GroupBy(ls => ls.DayOfWeek);
            var groupedOnlineLectureSchedules = onlineLectureSchedules.GroupBy(ls => ls.DayOfWeek);

            // Add worksheets for each day of the week
            foreach (var group in groupedLectureSchedules)
            {
                var dayOfWeek = group.Key;
                var worksheet = excelPackage.Workbook.Worksheets.Add(dayOfWeek.ToString());

                // Set up the headers
                worksheet.Cells["A1"].Value = "Time Period";
                worksheet.Cells["B1"].Value = "Room";
                worksheet.Cells["C1"].Value = "Lecture";

                var row = 2;

                // Add lecture schedules for the day
                foreach (var lectureSchedule in group)
                {
                    worksheet.Cells[$"A{row}"].Value = lectureSchedule.TimePeriod;
                    worksheet.Cells[$"B{row}"].Value = lectureSchedule.Room?.Name;
                    worksheet.Cells[$"C{row}"].Value = lectureSchedule.Lecture?.Course?.Name;

                    row++;
                }
            }

            // Add online lecture schedules
            foreach (var groupedOnlineLectureSchedule in groupedOnlineLectureSchedules)
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add($"Online - {groupedOnlineLectureSchedule.Key}");

                // Set up the headers
                worksheet.Cells["A1"].Value = "Time Period";
                worksheet.Cells["B1"].Value = "Lectures";

                var row = 2;

                foreach (var on in groupedOnlineLectureSchedule)
                {
                    // Add online lectures
                    foreach (var lecture in on.Lectures)
                    {
                        worksheet.Cells[$"A{row}"].Value = on.TimePeriod;
                        worksheet.Cells[$"B{row}"].Value = lecture?.Course?.Name;

                        row++;
                    }
                }
            }

            // Save the Excel file
            await excelPackage.SaveAsync();
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