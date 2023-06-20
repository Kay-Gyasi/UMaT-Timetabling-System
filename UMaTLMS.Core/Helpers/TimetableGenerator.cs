using LinqKit;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Helpers
{
    public static class TimetableGenerator
    {
        public static (List<LectureSchedule>, List<OnlineLectureSchedule>) Generate(List<LectureSchedule> schedules, List<OnlineLectureSchedule> onlineSchedules, List<Lecture> lectures)
        {

            Shuffle(schedules);
            lectures = lectures.OrderByDescending(x => x.Duration)
                .OrderBy(x => x.PreferredRoom is not null)
                .ToList();

            foreach (var lecture in lectures)
            {
                var builder = PredicateBuilder.New<LectureSchedule>(x => true);
                var onlineBuilder = PredicateBuilder.New<OnlineLectureSchedule>(x => true);

                for (var i = 0; i < 5; i++)
                {
                    var numOfLecturesForLecturer = schedules.Count(x => x.DayOfWeek == AppHelper.GetDayOfWeek(i)
                                                                        && (x.FirstLecture?.LecturerId != null
                                                                        && x.FirstLecture.LecturerId == lecture.LecturerId ||
                                                                        x.SecondLecture?.LecturerId != null
                                                                        && x.SecondLecture.LecturerId == lecture.LecturerId))
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
                LectureSchedule? schedule;

                if (lecture.Duration == 2)
                {
                    schedule = eligibleSchedules
                        .OrderBy(x => x.Room.Name == lecture.PreferredRoom)
                        .FirstOrDefault(x => x.FirstLectureId == null && x.SecondLectureId == null);
                    schedule?.HasLecture(lecture.Id, lecture.Id);
                    continue;
                }

                var eligibleSchedulesForOnePeriodLectures = eligibleSchedules.Where(x => x.FirstLectureId == null || x.SecondLectureId == null);
                if (eligibleSchedulesForOnePeriodLectures.Any())
                {
                    schedule = eligibleSchedulesForOnePeriodLectures
                        .OrderBy(x => x.Room.Name == lecture.PreferredRoom)
                        .FirstOrDefault();
                    if (schedule?.FirstLectureId is null)
                    {
                        schedule?.HasLecture(lecture.Id, null);
                        continue;
                    }

                    if (schedule?.SecondLectureId is null) schedule?.HasLecture(null, lecture.Id);
                }
            }

            return (schedules, onlineSchedules);
        }

        public static async Task GetTimetable(IExcelReader excelReader, IEnumerable<LectureSchedule> lectureSchedules, IEnumerable<OnlineLectureSchedule> onlineLectureSchedules, List<ClassRoom> rooms, string file)
        {
            if (string.IsNullOrWhiteSpace(file)) return;
            using var excelPackage = excelReader.CreateNew(file);

            var groupedLectureSchedules = lectureSchedules.GroupBy(ls => ls.DayOfWeek);
            var groupedOnlineLectureSchedules = onlineLectureSchedules.GroupBy(ls => ls.DayOfWeek);

            foreach (var group in groupedLectureSchedules)
            {
                var dayOfWeek = group.Key;
                var worksheet = excelPackage.Workbook.Worksheets.Add(dayOfWeek.ToString());

                BuildTimetableLayout(worksheet, dayOfWeek?.ToString() ?? "", rooms);

                var row = 2;

                // Add lecture schedules for the day
                //foreach (var lectureSchedule in group)
                //{
                //    worksheet.Cells[$"A{row}"].Value = lectureSchedule.TimePeriod;
                //    worksheet.Cells[$"B{row}"].Value = lectureSchedule.Room?.Name;
                //    //worksheet.Cells[$"C{row}"].Value = lectureSchedule.Lecture?.Course?.Name;

                //    row++;
                //}
            }

            await excelPackage.SaveAsync();
        }

        private static void BuildTimetableLayout(ExcelWorksheet worksheet, string dayOfWeek, List<ClassRoom> rooms)
        {
            // first row
            worksheet.Cells["A2"].Value = "University of Mines and Technology, Tarkwa".ToUpper();
            var cell1Range = worksheet.Cells["A2:M2"];
            cell1Range.ApplyStyling(14, isBold: true, isMerge: true);

            // second row
            worksheet.Cells["A3"].Value = "Semester {Semester No.} {Academic Year} Time Table".ToUpper();
            var cell2Range = worksheet.Cells["A3:M3"];
            cell2Range.ApplyStyling(14, isMerge: true);

            // classroom
            worksheet.Cells["A5"].Value = "Classroom".ToUpper();
            var c3Range = worksheet.Cells["A5:A7"];
            c3Range.ApplyStyling(isBold: true, isMerge: true);

            for(int i = 0; i < rooms.Count; i++)
            {
                worksheet.Cells[$"A{i + 8}"].Value = rooms[i]?.Name ?? "";
                worksheet.Cells[$"A{i + 8}"].ApplyStyling(isBold: true);
            }

            // Day
            worksheet.Cells["C5"].Value = dayOfWeek.ToUpper();
            var cell4Range = worksheet.Cells["C5:M5"];
            cell4Range.ApplyStyling(14, isBold: true, isMerge: true);

            var cells = GetColumns();
            for (int i = 2; i <= 12; i++)
            {
                worksheet.Cells[string.Join("", cells[i], 6)].Value = i - 1;
            }
            worksheet.Cells["C6:M6"].ApplyStyling();

            var timeslots = GetTimeSlots();
            for (int i = 1; i <= 12; i++)
            {
                worksheet.Cells[string.Join("", cells[i], 7)].Value = timeslots[i - 1];
            }
            worksheet.Cells["B7:M7"].ApplyStyling();
        }

        private static void ApplyStyling(this ExcelRange range, int fontSize = 10, bool isBold = false, bool isMerge = false)
        {
            range.AutoFitColumns();
            range.Style.Font.Name = "Arial";
            range.Style.Font.Size = fontSize;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            range.Style.Font.Bold = isBold;
            range.Merge = isMerge;
        }

        private static List<string> GetColumns()
        {
            return new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
        }

        private static List<string> GetTimeSlots()
        {
            return new List<string> { "6:00-7:00", "7:00-8:00", "8:00-9:00", "9:00-10:00", "10:00-11:00", "11:00-12:00", "12:30-1:30",
            "1:30-2:30", "2:30-3:30", "3:30-4:30", "4:30-5:30", "5:30-6:30"};
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