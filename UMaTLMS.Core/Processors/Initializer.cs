using OfficeOpenXml;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Processors;

[Processor]
public class Initializer
{
    private readonly IExcelReader _reader;
    private readonly RoomProcessor _roomProcessor;
    private readonly DepartmentProcessor _departmentProcessor;
    private ExcelWorksheet _firstSemesterWorksheet;
    private const string SecondSemFile = "_content/DRAFT TIME TABLE SEM ONE 2022_2023.xlsx";

    public Initializer(IExcelReader reader, RoomProcessor roomProcessor,
        DepartmentProcessor departmentProcessor)
    {
        _reader = reader;
        _roomProcessor = roomProcessor;
        _departmentProcessor = departmentProcessor;
    }

    public async Task Initialize()
    {
        var currentWorksheet = 0;

        while (currentWorksheet < _reader.NoOfWorkSheets(SecondSemFile) - 2)
        {
            _firstSemesterWorksheet = _reader.GetWorkSheet(SecondSemFile, currentWorksheet);

            InitializeSemesters();
            await InitializeDepartments();
            InitializeLecturers();
            InitializeCourses();
            await InitializeRooms();

            currentWorksheet += 1;
        }
    }

    private void InitializeSemesters()
    {
        //
    }

    private async Task InitializeDepartments()
    {
        for (var row = 8; row < _firstSemesterWorksheet.Dimension.End.Row; row++)
        {
            for (var col = 2; col < _firstSemesterWorksheet.Dimension.End.Column; col++)
            {
                var cellValue = _firstSemesterWorksheet.Cells[row, col].Value?.ToString()?.TrimStart();
                if (string.IsNullOrEmpty(cellValue)) continue;

                var code = cellValue.Split(" ")[0];
                if (code.EndsWith(','))
                    code = code[..^1];
                if (await _departmentProcessor.Exists(code)) continue;

                var command = new DepartmentCommand(0, code, code);

                _ = await _departmentProcessor.UpsertAsync(command);
            }
        }
    }

    private void InitializeLecturers()
    {
        //
    }

    private void InitializeCourses()
    {
        //
    }

    private async Task InitializeRooms()
    {
        for (var row = 8; row < _firstSemesterWorksheet.Dimension.End.Row; row++)
        {
            var cellValue = _firstSemesterWorksheet.Cells[row, 1].Value?.ToString()?.Trim();
            if (string.IsNullOrEmpty(cellValue)) continue;

            int? capacity = null;
            string[]? split = null;
            if (cellValue.Contains('('))
            {
                split = cellValue.Split('(');
                var isParsed = int.TryParse(split[1][..^1], out var i);
                if (isParsed) capacity = i;
                else continue;
            }

            var name = split is not null ? split[0].Trim() : cellValue;
            if (await _roomProcessor.Exists(name)) continue;

            var isLab = split is not null ? split[0].Contains("LAB") || split[0].Contains("MEDIA ROOM")
                : cellValue.Contains("LAB") || cellValue.Contains("MEDIA ROOM");
            var isWorkshop = split is not null ? split[0].Contains("WORKSHOP")
                : cellValue.Contains("WORKSHOP");

            var command = new RoomCommand(0, name, capacity, isLab, isWorkshop);

            _ = await _roomProcessor.UpsertAsync(command);
        }
    }
}