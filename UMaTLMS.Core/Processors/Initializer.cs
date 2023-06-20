using OfficeOpenXml;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class Initializer
{
    private readonly IExcelReader _reader;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private readonly IOnlineLectureScheduleRepository _onlineLectureScheduleRepository;
    private ExcelWorksheet _firstSemesterWorksheet;
    private const string _firstSemFile = "_content/DRAFT TIME TABLE SEM ONE 2022_2023.xlsx";

    public Initializer(IExcelReader reader, IRoomRepository roomRepository, 
        ILectureScheduleRepository lectureScheduleRepository, IOnlineLectureScheduleRepository onlineLectureScheduleRepository)
    {
        _reader = reader;
        _roomRepository = roomRepository;
        _lectureScheduleRepository = lectureScheduleRepository;
        _onlineLectureScheduleRepository = onlineLectureScheduleRepository;
    }

    public async Task Initialize()
    {
        var currentWorksheet = 0;
        while (currentWorksheet < _reader.NoOfWorkSheets(_firstSemFile) - 2)
        {
            _firstSemesterWorksheet = _reader.GetWorkSheet(_firstSemFile, currentWorksheet);
            await InitializeRooms();
            currentWorksheet += 1;
        }
    }

    private async Task InitializeRooms()
    {
        if (await _roomRepository.IsInitialized()) return;
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

            var name = split is not null ? split[0].Trim() : cellValue.Trim();
            name = SetNames(name);
            if(name is null) continue;
            if (await _roomRepository.Exists(name)) continue;

            var isLab = split is not null ? split[0].Contains("LAB") || split[0].Contains("MEDIA ROOM") || split[0].Contains("WORKSHOP")
                : cellValue.Contains("LAB") || cellValue.Contains("MEDIA ROOM") || cellValue.Contains("WORKSHOP");
            
            var command = ClassRoom.Create(name, capacity ?? 0);
            command.IsLabRoom(isLab);

            var excludedNames = GetRoomsExcludedFromGeneralAssignment();
            if (excludedNames.Any(x => x == command.Name)) command.IsExcludedFromGeneralAssignment();

            await _roomRepository.AddAsync(command, false);
        }

        await _roomRepository.SaveChanges();
    }

    private static string? SetNames(string name)
    {
        switch (name.Trim())
        {
            case "COMPUTER  ROOM":
            case "VLE":
            case "Mini                                   Auditorium":
                return null;
            case "Mini                                                   Auditorium":
                name = "Mini Auditorium";
                break;
        }

        return name;
    }

    private static List<string> GetRoomsExcludedFromGeneralAssignment()
    {
        return new List<string> { "FIELD WORK 1", "FIELD WORK 2", "FIELD WORK 3", "LIBRARY", "FRENCH MULTI MEDIA ROOM" };
    }
}