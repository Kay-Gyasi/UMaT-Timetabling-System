using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Text;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Helpers;

public static partial class TimetableGenerator
{
    public static async Task GetAsync(IExcelReader excelReader, IEnumerable<LectureSchedule> lectureSchedules, 
        IEnumerable<OnlineLectureSchedule> onlineLectureSchedules, List<ClassRoom> rooms, string file)
    {
        if (string.IsNullOrWhiteSpace(file)) return;
        using var excelPackage = excelReader.CreateNew(file);

        BuildWorksheetsAndAddGeneralLectures(lectureSchedules, excelPackage, rooms);
        AddVleLectures(onlineLectureSchedules, excelPackage);
        await excelPackage.SaveAsync();
    }

    private static void BuildWorksheetsAndAddGeneralLectures(IEnumerable<LectureSchedule> lectureSchedules, 
        ExcelPackage package, List<ClassRoom> rooms)
    {
        var groupedLectureSchedules = lectureSchedules.GroupBy(ls => ls.DayOfWeek);
        foreach (var group in groupedLectureSchedules)
        {
            AddLecturesForDay(group, rooms, package);
        }
    }

    private static void AddLecturesForDay(IGrouping<DayOfWeek?, LectureSchedule> group, List<ClassRoom> rooms, ExcelPackage package)
    {
        var dayOfWeek = group.Key;
        var worksheet = package.Workbook.Worksheets.Add(dayOfWeek.ToString());
        BuildWorksheetLayout(worksheet, dayOfWeek?.ToString() ?? string.Empty, rooms);

        var schedules = group.Where(x => x.FirstLecture != null || x.SecondLecture != null).ToList();
        var columns = GetColumns();

        foreach (var lectureSchedule in schedules)
        {
            bool entryHasBeenMade = MakeEntryForTwoPeriodSchedule(worksheet, lectureSchedule, rooms, columns);
            if (entryHasBeenMade) continue;

            MakeEntryForOnePeriodSchedule(worksheet, lectureSchedule, rooms, columns);
        }
    }


    private static void AddVleLectures(IEnumerable<OnlineLectureSchedule> onlineLectureSchedules, 
        ExcelPackage package)
    {
        var groupedOnlineLectureSchedules = onlineLectureSchedules.GroupBy(ls => ls.DayOfWeek);
        foreach (var group in groupedOnlineLectureSchedules)
        {
            var worksheet = AddOnlineLecturesForDay(group, package);
            if (worksheet is null) continue;

            RemoveRedundantCells(worksheet);
        }
    }

    private static ExcelWorksheet? AddOnlineLecturesForDay(IGrouping<DayOfWeek?, OnlineLectureSchedule> group, ExcelPackage package)
    {
        var dayOfWeek = group.Key;
        var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == dayOfWeek.ToString());
        if (worksheet is null) return null;

        AddVleRow(worksheet);
        foreach (var onlineSchedule in group)
        {
            foreach (var lecture in onlineSchedule.Lectures)
            {
                var builder = new StringBuilder();

                var cellName = GetVleCellName(worksheet, onlineSchedule, ("", ""), GetColumns());
                SetCellValue(lecture, builder, worksheet, cellName);
            }
        }

        return worksheet;
    }

    private static bool MakeEntryForTwoPeriodSchedule(ExcelWorksheet worksheet, LectureSchedule lectureSchedule,
        List<ClassRoom> rooms, List<string> columns)
    {
        if (lectureSchedule.FirstLectureId != lectureSchedule.SecondLectureId) return false;

        var builder = new StringBuilder();
        var first = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, lectureNo: 1);
        var second = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, lectureNo: 2);

        try
        {
            SetCellValue(lectureSchedule.FirstLecture!, builder, worksheet, first, second);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return true;
    }

    private static void MakeEntryForOnePeriodSchedule(ExcelWorksheet worksheet, LectureSchedule lectureSchedule,
        List<ClassRoom> rooms, List<string> columns)
    {
        string cellName, cellValue = string.Empty;
        var builder = new StringBuilder();

        if (lectureSchedule.FirstLecture is not null)
        {
            cellName = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 1);
            try
            {
                SetCellValue(lectureSchedule.FirstLecture!, builder, worksheet, cellName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        if (lectureSchedule.SecondLecture is not null)
        {
            builder = new StringBuilder();
            cellName = GetCellName(worksheet, ("", ""), lectureSchedule, columns, rooms.Count, 2);
            try
            {
                SetCellValue(lectureSchedule.SecondLecture!, builder, worksheet, cellName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    private static void BuildWorksheetLayout(ExcelWorksheet worksheet, string dayOfWeek, List<ClassRoom> rooms)
    {

        worksheet.Columns.Width = 16.30;
        worksheet.Rows.Height = 40;

        for (int i = 1; i < 7; i++)
        {
            worksheet.Row(i).Height = 15;
        }

        SetHeadersAndRooms(worksheet, rooms);
        SetDayAndPeriods(worksheet, dayOfWeek);
    }

    private static void AddVleRow(ExcelWorksheet worksheet)
    {
        var row = worksheet.Dimension.End.Row + 1;
        worksheet.Cells[$"A{row}"].Value = "VLE";
        worksheet.Cells[$"A{row}:M{row}"].ApplyStyling(isBold: true);
        worksheet.Cells[$"A{row}:M{row}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        worksheet.Cells[$"A{row}:M{row}"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
    }

    private static void SetDayAndPeriods(ExcelWorksheet worksheet, string dayOfWeek)
    {
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

    private static void SetHeadersAndRooms(ExcelWorksheet worksheet, List<ClassRoom> rooms)
    {
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

        for (int i = 0; i < rooms.Count; i++)
        {
            worksheet.Cells[$"A{i + 8}"].Value = rooms[i]?.Name ?? "";
            worksheet.Cells[$"A{i + 8}"].ApplyStyling(isBold: true);
        }
    }

    private static void RemoveRedundantCells(ExcelWorksheet worksheet)
    {
        // do work here
        var border = worksheet.Cells[$"{worksheet.Cells.Start.Address}:{worksheet.Dimension.End.Address}"].Style.Border;
        border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = ExcelBorderStyle.Thin;
    }

    private static string GetCellName(ExcelWorksheet worksheet, (string Column, string Row) cellName,
        LectureSchedule lectureSchedule, List<string> columns, int roomsCount, int lectureNo)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            if (!string.IsNullOrWhiteSpace(cellName.Column) && !string.IsNullOrWhiteSpace(cellName.Row)) break;

            for (int j = 7; j <= roomsCount + 7; j++)
            {
                if (!string.IsNullOrWhiteSpace(cellName.Column) && !string.IsNullOrWhiteSpace(cellName.Row)) break;

                var cellValue = worksheet.Cells[$"{columns[i]}{j}"].Value?.ToString();
                if (string.IsNullOrEmpty(cellValue)) continue;

                if (cellValue == GetTimeMapping(lectureSchedule.TimePeriod, lectureNo))
                {
                    cellName.Column = columns[i];
                }

                if (cellValue == lectureSchedule.Room.Name)
                {
                    cellName.Row = j.ToString();
                }
            }
        }

        return $"{cellName.Column}{cellName.Row}";
    }

    private static string GetVleCellName(ExcelWorksheet worksheet, OnlineLectureSchedule lectureSchedule,
        (string Column, string Row) cellName, List<string> columns)
    {
        List<int> vleRows = new();
        string result;

        for (int k = 1; k <= worksheet.Dimension.End.Row; k++)
        {
            if (worksheet.Cells[$"A{k}"].Value?.ToString() != "VLE") continue;
            vleRows.Add(k);
        }

        foreach (var row in vleRows)
        {
            if (!string.IsNullOrWhiteSpace(cellName.Column) && !string.IsNullOrWhiteSpace(cellName.Row)) break;

            for (int i = 0; i < columns.Count; i++)
            {
                var cellValue = worksheet.Cells[$"{columns[i]}{7}"].Value?.ToString(); // 7th row has the time periods
                if (string.IsNullOrEmpty(cellValue)) continue;

                if (cellValue != GetTimeMapping(lectureSchedule.TimePeriod, 1)) continue;
                var name = $"{columns[i]}{row}";
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[name].Value?.ToString())) break;

                cellName.Column = columns[i];
                cellName.Row = row.ToString();
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(cellName.Column))
        {
            AddVleRow(worksheet);
            result = GetVleCellName(worksheet, lectureSchedule, cellName, columns);
            return result;
        }

        return $"{cellName.Column}{cellName.Row}";
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
        var names = lecture.SubClassGroups.Select(x => x.Name).ToList();
        var modifiedNames = new List<string>();
        foreach (var name in names)
        {
            if (int.TryParse(name[^1].ToString(), out _))
            {
                modifiedNames.Add(name.Split(AppHelpers.WhiteSpace)[0]);
                continue;
            }

            modifiedNames.Add(name);
        }

        builder.Append(string.Join(", ", modifiedNames));
        builder.Append(' ');
        builder.Append(lecture.Course?.Code!.Split(AppHelpers.WhiteSpace)[1]);
        builder.Append(lecture.IsPractical ? " (P)" : "");
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
            "1:30-2:30", "2:30-3:30", "3:30-4:30", "4:30-5:30", "5:30-6:30" };
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
}
