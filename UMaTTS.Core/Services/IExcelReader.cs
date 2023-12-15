using OfficeOpenXml;

namespace UMaTLMS.Core.Services;

public interface IExcelReader
{
    ExcelPackage CreateNew(string filePath);
    ExcelWorksheet GetWorkSheet(string filePath, string worksheet);
    ExcelWorksheet GetWorkSheet(string filePath, int index);
    string? GetCellValue(ExcelWorksheet worksheet, int row, string col);
    int NoOfWorkSheets(string filePath);
    Task SaveAsync();
}
