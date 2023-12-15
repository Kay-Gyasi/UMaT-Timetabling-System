using OfficeOpenXml;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure;

public sealed class ExcelReader : IExcelReader
{
    private ExcelPackage _package;
    public ExcelReader()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public ExcelPackage CreateNew(string filePath) => new ExcelPackage(new FileInfo(filePath));

    public ExcelWorksheet GetWorkSheet(string filePath, string worksheet)
    {
        _package = _package.SetFilePath(filePath);
        return _package.Workbook.Worksheets[worksheet];
    }

    public ExcelWorksheet GetWorkSheet(string filePath, int index)
    {
        _package = _package.SetFilePath(filePath);
        return _package.Workbook.Worksheets[index];
    }

    public string? GetCellValue(ExcelWorksheet worksheet, int row, string col)
    {
        return worksheet.Cells[$"{col}{row}"].Value?.ToString();
    }

    public int NoOfWorkSheets(string filePath)
    {
        _package = _package.SetFilePath(filePath);
        return _package.Workbook.Worksheets.Count;
    }

    public async Task SaveAsync()
    {
        await _package.SaveAsync();
    }
}

public static class ExcelReaderExtensions
{
    public static ExcelPackage SetFilePath(this ExcelPackage package, string filePath)
    {
        return new ExcelPackage(new FileInfo(filePath));
    }
}