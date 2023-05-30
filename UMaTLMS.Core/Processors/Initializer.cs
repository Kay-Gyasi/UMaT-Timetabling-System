using OfficeOpenXml;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class Initializer
{
    private readonly IExcelReader _reader;
    private readonly RoomProcessor _roomProcessor;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private ExcelWorksheet _firstSemesterWorksheet;
    private const string _firstSemFile = "_content/DRAFT TIME TABLE SEM ONE 2022_2023.xlsx";

    public Initializer(IExcelReader reader, RoomProcessor roomProcessor,
        IRoomRepository roomRepository, ILectureScheduleRepository lectureScheduleRepository)
    {
        _reader = reader;
        _roomProcessor = roomProcessor;
        _roomRepository = roomRepository;
        _lectureScheduleRepository = lectureScheduleRepository;
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

        await InitializeLectureSchedule();
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

            var name = split is not null ? split[0].Trim() : cellValue;
            name = SetNames(name);
            if(name is null) continue;
            if (await _roomProcessor.Exists(name)) continue;

            var isLab = split is not null ? split[0].Contains("LAB") || split[0].Contains("MEDIA ROOM")
                : cellValue.Contains("LAB") || cellValue.Contains("MEDIA ROOM");
            var isWorkshop = split is not null ? split[0].Contains("WORKSHOP")
                : cellValue.Contains("WORKSHOP");
            
            capacity ??= SetCapacities(name);
            var command = new RoomCommand(0, name, capacity, isLab, isWorkshop);
            _ = await _roomProcessor.UpsertAsync(command);
        }
    }

    private async Task InitializeLectureSchedule()
    {
        var rooms = (await _roomRepository.GetAllAsync()).ToList();
        var timeSlots = GetTimeSLots().ToList();
        
        for (var i = 0; i < 5; i++)
        {
            foreach (var room in rooms)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var schedule = LectureSchedule.Create(AppHelper.GetDayOfWeek(i), timeSlot, room.Id);
                    if (i == 4 && timeSlot is "4:30pm" or "6:30pm") continue;
                    await _lectureScheduleRepository.AddAsync(schedule);
                }
            }
        }
    }

    private static int SetCapacities(string name)
    {
        switch (name)
        {
            case "LH 5":
            case "ED I":
            case "ED II":
            case "ED III":
            case "MS 1":
            case "MS 2":
            case "ELECT. LAB.":
            case "COMPUTER ROOM":
            case "PETROLEUM ROOM":
            case "COMP ENG LAB":
            case "SOFTWARE LAB":
            case "MINING LAB":
            case "GIS/PET LAB":
            case "GEOL. LAB.":
            case "MECH. LAB.":
            case "MECH. WORKSHOP":
            case "MINER. ENG. LAB.":
            case "MINING COMP LAB":
            case "GIS/PETROLEUM":
                return 70;
            case "Mini Auditorium":
            case "VLE":
            case "FIELD WORK 1":
            case "FIELD WORK 2":
            case "FIELD WORK 3":
            case "LIBRARY":
                return 400;
            case "Auditorium Foyer":
            case "GE 1":
                return 50;
            case "MRT I":
            case "MRT II":
            case "MRT III":
            case "CB 2":
            case "CL 1":
            case "MC 1":
                return 100;
            case "FRENCH MULTI MEDIA ROOM":
                return 25;
            default:
                return 70;
        }
    }

    private static string? SetNames(string name)
    {
        switch (name)
        {
            case "COMPUTER  ROOM":
            case "FIELD WORK 1":
            case "FIELD WORK 2":
            case "FIELD WORK 3":
            case "LIBRARY":
            case "Auditorium Foyer":
            case "FRENCH MULTI MEDIA ROOM":
            case "Mini                                   Auditorium":
                return null;
            case "Mini                                                   Auditorium":
                name = "Mini Auditorium";
                break;
        }

        return name;
    }

    private static IEnumerable<string> GetTimeSLots()
    {
        return new List<string>()
        {
            "6am",
            "8am",
            "10am",
            "12:30pm",
            "2:30pm",
            "4:30pm",
            "6:30pm"
        };
    }
}