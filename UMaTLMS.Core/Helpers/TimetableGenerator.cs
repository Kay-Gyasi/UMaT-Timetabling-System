using LinqKit;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UMaTLMS.Core.Entities;
using UMaTLMS.Core.Processors;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Helpers
{
    public static class TimetableGenerator
    {
        public static (List<LectureSchedule>, List<OnlineLectureSchedule>) Generate(List<LectureSchedule> schedules, List<OnlineLectureSchedule> onlineSchedules, List<Lecture> lectures)
        {
            if (schedules.Any(x => x.FirstLecture is not null || x.SecondLecture is not null))
                return (schedules, onlineSchedules);

            Shuffle(schedules);
            lectures = lectures.OrderByDescending(x => x.Duration)
                .OrderByDescending(x => x.PreferredRoom is not null)
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
                        .FirstOrDefault(x => x.FirstLectureId == null 
                        && x.SecondLectureId == null 
                        && x.Room.Name == lecture.PreferredRoom);

                    schedule ??= eligibleSchedules
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

                // fill in lectures
                var schedules = group.Where(x => x.FirstLecture != null || x.SecondLecture != null).ToList();
                var columns = GetColumns();
                foreach (var lectureSchedule in schedules)
                {
                    string cellName, cellValue = string.Empty;

                    var builder = new StringBuilder();

                    if (lectureSchedule.FirstLectureId == lectureSchedule.SecondLectureId)
                    {
                        var first = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 1);
                        var second = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 2);
                        SetCellValue(lectureSchedule.FirstLecture!, builder, worksheet, first, second);
                        continue;
                    }

                    if (lectureSchedule.FirstLecture is not null)
                    {
                        cellName = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 1);
                        SetCellValue(lectureSchedule.FirstLecture!, builder, worksheet, cellName);
                    }

                    if (lectureSchedule.SecondLecture is not null)
                    {
                        builder = new StringBuilder();
                        cellName = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 2);
                        SetCellValue(lectureSchedule.SecondLecture!, builder, worksheet, cellName);
                    }
                }
            }

            foreach (var group in groupedOnlineLectureSchedules)
            {
                var dayOfWeek = group.Key;
                var worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(x => x.Name == dayOfWeek.ToString());
                if (worksheet is null) continue;

                AddVleRow(worksheet);

                foreach (var onlineSchedule in group)
                {
                    foreach (var lecture in onlineSchedule.Lectures)
                    {
                        string cellName;
                        var builder = new StringBuilder();

                        cellName = GetVleCellName(worksheet, onlineSchedule, ("", ""), GetColumns());
                        SetCellValue(lecture, builder, worksheet, cellName);
                    }
                }

                RemoveRedundantCells(worksheet); // not working
            }

            await excelPackage.SaveAsync();
        }

        private static void BuildTimetableLayout(ExcelWorksheet worksheet, string dayOfWeek, List<ClassRoom> rooms)
        {

            worksheet.Columns.Width = 16.30;
            worksheet.Rows.Height = 40;

            for (int i = 1; i < 7; i++)
            {
                worksheet.Row(i).Height = 15;
            }

            // first row
            worksheet.Cells["A1"].Value = "University of Mines and Technology, Tarkwa".ToUpper();
            var cell1Range = worksheet.Cells["A1:M2"];
            cell1Range.ApplyStyling(14, isBold: true, isMerge: true);

            // second row
            worksheet.Cells["A3"].Value = "Semester {Semester No.} {Academic Year} Time Table".ToUpper();
            var cell2Range = worksheet.Cells["A3:M4"];
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

        private static void AddVleRow(ExcelWorksheet worksheet)
        {
            var row = worksheet.Dimension.End.Row + 1;
            worksheet.Cells[$"A{row}"].Value = "VLE";
            worksheet.Cells[$"A{row}:M{row}"].ApplyStyling(isBold: true);
            worksheet.Cells[$"A{row}:M{row}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{row}:M{row}"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        }

        private static void RemoveRedundantCells(ExcelWorksheet worksheet)
        {
            int lastRow = worksheet.Dimension.End.Row, lastColumn = worksheet.Dimension.End.Column;

            worksheet.Rows[lastRow + 1, worksheet.Cells.End.Row - lastRow].Style.Fill.SetBackground(Color.Gray);
            // worksheet.Columns[lastColumn + 1, worksheet.Cells.End.Column - lastColumn + 1].Style.Fill.SetBackground(Color.Gray);

            var border = worksheet.Cells[$"{worksheet.Cells.Start.Address}:{worksheet.Dimension.End.Address}"].Style.Border;
            border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private static string GetCellName(ExcelWorksheet worksheet, (string, string) cellName,
            LectureSchedule lectureSchedule, List<string> columns, int roomsCount, int lectureNo)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(cellName.Item1) && !string.IsNullOrWhiteSpace(cellName.Item2)) break;

                for (int j = 7; j < roomsCount + 7; j++)
                {
                    if (!string.IsNullOrWhiteSpace(cellName.Item1) && !string.IsNullOrWhiteSpace(cellName.Item2)) break;

                    var cellValue = worksheet.Cells[$"{columns[i]}{j}"].Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue)) continue;

                    if (cellValue == GetTimeMapping(lectureSchedule.TimePeriod, lectureNo))
                    {
                        cellName.Item1 = columns[i];
                    }

                    if (cellValue == lectureSchedule.Room.Name)
                    {
                        cellName.Item2 = j.ToString();
                    }
                }
            }

            return $"{cellName.Item1}{cellName.Item2}";
        }

        private static string GetVleCellName(ExcelWorksheet worksheet, OnlineLectureSchedule lectureSchedule, 
            (string, string) cellName, List<string> columns)
        {
            List<int> vleRows = new List<int>();
            string result;

            for (int k = 1; k <= worksheet.Dimension.End.Row; k++)
            {
                if (worksheet.Cells[$"A{k}"].Value?.ToString() != "VLE") continue;
                vleRows.Add(k);
            }

            foreach (var row in vleRows)
            {
                if (!string.IsNullOrWhiteSpace(cellName.Item1) && !string.IsNullOrWhiteSpace(cellName.Item2)) break;

                for (int i = 0; i < columns.Count; i++)
                {
                    var cellValue = worksheet.Cells[$"{columns[i]}{i}"].Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue)) continue;

                    if (cellValue == GetTimeMapping(lectureSchedule.TimePeriod, 1))
                    {
                        var name = $"{columns[i]}{i}";
                        if (!string.IsNullOrWhiteSpace(worksheet.Cells[name].Value?.ToString())) break;

                        cellName.Item1 = columns[i];
                        cellName.Item2 = i.ToString();
                        break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(cellName.Item1))
            {
                AddVleRow(worksheet);
                result = GetVleCellName(worksheet, lectureSchedule, cellName, columns);
                return result;
            }

            return $"{cellName.Item1}{cellName.Item2}";
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

        private static void SetCellValue(Lecture lecture, StringBuilder builder, ExcelWorksheet worksheet,
            string first, string? second = null)
        {
            var names = lecture.SubClassGroups.Select(x => x.Name);
            builder.Append(string.Join(",", names));
            builder.Append(' ');
            builder.Append(lecture.Course?.Code!.Split(" ")[1]);
            builder.Append(lecture.IsPractical == true ? " (P)" : "");
            builder.Append(Environment.NewLine);
            builder.Append(lecture.Lecturer?.Name?.Split(",").First());

            ExcelRange cell;
            var cellValue = builder.ToString();
            if (second is not null) cell = worksheet.Cells[$"{first}:{second}"];
            else cell = worksheet.Cells[$"{first}"];
            cell.Value = cellValue;
            cell.ApplyStyling(isMerge: true);
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

        private static string GetTimeMapping(string key, int period)
        {
            var firstPeriodMapping = new Dictionary<string, string>
            {
                { "6am", "6:00-7:00" },
                { "8am", "8:00-9:00" },
                { "10am", "10:00-11:00" },
                { "12:30pm", "12:30-1:30" },
                { "2:30pm", "2:30-3:30" },
                { "4:30pm", "4:30-5:30" },
            };

            var secondPeriodMapping = new Dictionary<string, string>
            {
                { "6am", "7:00-8:00" },
                { "8am", "9:00-10:00" },
                { "10am", "11:00-12:00" },
                { "12:30pm", "1:30-2:30" },
                { "2:30pm", "3:30-4:30" },
                { "4:30pm", "5:30-6:30" },
            };

            if (period == 1) return firstPeriodMapping[key];
            return secondPeriodMapping[key];
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