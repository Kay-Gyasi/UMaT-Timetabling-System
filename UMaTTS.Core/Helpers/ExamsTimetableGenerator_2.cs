using Humanizer;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Helpers;

public static partial class ExamsTimetableGenerator
{
    public static async Task GetAsync(IExcelReader excelReader, List<List<ExamsSchedule>> schedules, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return;
        using var excelPackage = excelReader.CreateNew(fileName);

        var worksheets = Enum.GetValues<ExamWorksheets>();
        List<ExamsSchedule> groupedExamsSchedules;
        ExcelWorksheet worksheet;
        foreach (var examWorksheet in worksheets)
        {
            switch (examWorksheet)
            {
                case ExamWorksheets.General:
                    groupedExamsSchedules = schedules[0].OrderBy(x => x.DateOfExam).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Courses:
                    groupedExamsSchedules = schedules[0].OrderBy(x => x.CourseName).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Examiners:
                    groupedExamsSchedules = schedules[0].OrderBy(x => x.Examiner).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Invigilators:
                    groupedExamsSchedules = schedules[0].OrderBy(x => x.Invigilators.FirstOrDefault()?.Name).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelPackage, worksheet, groupedExamsSchedules);
                    break;
                case ExamWorksheets.Practicals:
                    groupedExamsSchedules = schedules[1].OrderBy(x => x.Invigilators.FirstOrDefault()?.Name).ToList();
                    worksheet = excelPackage.Workbook.Worksheets.Add(examWorksheet.Humanize());
                    await BuildWorksheet(excelPackage, worksheet, groupedExamsSchedules);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static async Task BuildWorksheet(ExcelPackage excelPackage, ExcelWorksheet worksheet,
    IEnumerable<ExamsSchedule> schedules)
    {
        BuildLayout(worksheet);
        var currentColumn = 5;
        foreach (var schedule in schedules)
        {
            if (schedule.CourseCodes is null) continue;
            var groupCount = 0;
            foreach (var code in schedule.CourseCodes)
            {
                worksheet.Cells[$"A{currentColumn}"].Value = schedule.DateOfExam.ToLongDateString();
                worksheet.Cells[$"B{currentColumn}"].Value = code.ToUpper();
                worksheet.Cells[$"C{currentColumn}"].Value = schedule.CourseName?.ToUpper();
                worksheet.Cells[$"D{currentColumn}"].Value = schedule.SubClassGroups[groupCount].Name.ToUpper();
                worksheet.Cells[$"E{currentColumn}"].Value = schedule.SubClassGroups[groupCount].Size;
                worksheet.Cells[$"F{currentColumn}"].Value = schedule.Examiner?.ToUpper();
                worksheet.Cells[$"G{currentColumn}"].Value = schedule.Room?.Name.ToUpper();
                worksheet.Cells[$"H{currentColumn}"].Value = schedule.Invigilators[groupCount].Name?.ToUpper();
                worksheet.Cells[$"I{currentColumn}"].Value = schedule.ExamPeriod switch
                {
                    ExamPeriod.Morning => "M",
                    ExamPeriod.Afternoon => "A",
                    ExamPeriod.Evening => "E",
                    _ => "-"
                };
                groupCount += 1;
                currentColumn += 1;
            }
        }

        var cells = worksheet.Cells[$"A5:I{currentColumn}"];
        cells.ApplyStyling();
        var border = worksheet.Cells[$"A5:I{currentColumn}"].Style.Border;
        border.Left.Style = border.Right.Style = border.Top.Style = border.Bottom.Style = ExcelBorderStyle.Thin;
        await excelPackage.SaveAsync();
    }

    private static void BuildLayout(ExcelWorksheet worksheet)
    {
        // first row
        worksheet.Cells["A1"].Value = "EXAMINATIONS TIME TABLE".ToUpper();
        var cell1Range = worksheet.Cells["A1:I1"];
        cell1Range.ApplyStyling(12, isBold: true, isMerge: true);

        // second row
        worksheet.Cells["A2"].Value = "{Semester No.} {Academic Year}".ToUpper();
        var cell2Range = worksheet.Cells["A2:I2"];
        cell2Range.ApplyStyling(isBold: true, isMerge: true);

        // headers
        worksheet.Cells["A4"].Value = "Date".ToUpper();
        worksheet.Cells["B4"].Value = "Course No".ToUpper();
        worksheet.Cells["C4"].Value = "Course name".ToUpper();
        worksheet.Cells["D4"].Value = "Class".ToUpper();
        worksheet.Cells["E4"].Value = "No".ToUpper();
        worksheet.Cells["F4"].Value = "Lecturer".ToUpper();
        worksheet.Cells["G4"].Value = "Room".ToUpper();
        worksheet.Cells["H4"].Value = "Invigilator".ToUpper();
        worksheet.Cells["I4"].Value = "Period".ToUpper();
        var cell3Range = worksheet.Cells["A4:I4"];
        cell3Range.ApplyStyling(isBold: true);

        // column widths
        worksheet.Column(1).Width = 26.73;
        worksheet.Column(2).Width = 13.0;
        worksheet.Column(3).Width = 40.91;
        worksheet.Column(4).Width = 10.55;
        worksheet.Column(6).Width = 32.09;
        worksheet.Column(7).Width = 25.0;
        worksheet.Column(8).Width = 32.09;
        worksheet.Column(9).Width = 11.45;
    }

    private static string GetCourseNumber(string courseCode)
    {
        return courseCode.Split(AppHelpers.WhiteSpace)[1];
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
}
