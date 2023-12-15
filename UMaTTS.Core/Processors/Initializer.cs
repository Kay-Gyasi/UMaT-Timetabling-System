using OfficeOpenXml;
using System.Text.Json;
using UMaTLMS.Core.Repositories.Base;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Processors;

[Processor]
public sealed class Initializer
{
    private readonly IExcelReader _reader;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private readonly IOnlineLectureScheduleRepository _onlineLectureScheduleRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IConstraintRepository _constraintRepository;
    private readonly IClassGroupRepository _classGroupRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IPreferenceRepository _preferenceRepository;
    private readonly IAdminSettingsRepository _adminSettingsRepository;
    private readonly ILogger<Initializer> _logger;
    private ExcelWorksheet _firstSemesterWorksheet;
    private const string _firstSemFile = "_content/DRAFT TIME TABLE SEM ONE 2022_2023.xlsx";

    public Initializer(IExcelReader reader, IRoomRepository roomRepository, 
        ILectureScheduleRepository lectureScheduleRepository, IOnlineLectureScheduleRepository onlineLectureScheduleRepository, 
        ILectureRepository lectureRepository, ICourseRepository courseRepository, ILecturerRepository  lecturerRepository, 
        IConstraintRepository constraintRepository,IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository,
        IPreferenceRepository preferenceRepository, IAdminSettingsRepository adminSettingsRepository, ILogger<Initializer> logger) 
    {
        _reader = reader;
        _roomRepository = roomRepository;
        _lectureScheduleRepository = lectureScheduleRepository;
        _onlineLectureScheduleRepository = onlineLectureScheduleRepository;
        _lectureRepository = lectureRepository;
        _courseRepository = courseRepository;
        _lecturerRepository = lecturerRepository;
        _constraintRepository = constraintRepository;
        _classGroupRepository = classGroupRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _preferenceRepository = preferenceRepository;
        _adminSettingsRepository = adminSettingsRepository;
        _logger = logger;
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

        await AddBaseConstraints();
        await AddAdminSettings();

        await _adminSettingsRepository.SaveChanges();
    }

    public async Task Reset()
    {
        try
        {
            await DeleteAllAsync(_lectureScheduleRepository);
            await DeleteAllAsync(_onlineLectureScheduleRepository);
            await DeleteAllAsync(_preferenceRepository);
            await DeleteAllAsync(_constraintRepository);
            await DeleteAllAsync(_subClassGroupRepository);
            await DeleteAllAsync(_classGroupRepository);
            await DeleteAllAsync(_courseRepository);
            await DeleteAllAsync(_lecturerRepository);
            await DeleteAllAsync(_lectureRepository, saveChanges: true);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}", ex.ToString());
            throw;
        }
    }

    private async Task DeleteAllAsync<T>(IRepository<T, int> repository, bool saveChanges = false) where T : Entity
    {
        var entities = await repository.GetAllAsync();
        await repository.DeleteAllAsync(entities, saveChanges);
    }

    private async Task AddBaseConstraints()
    {
        var isInitialized = await _constraintRepository.IsInitialized();
        if (isInitialized) return;

        var maxLecturesPerDayConstraint = Constraint
                                            .Create(ConstraintType.GeneralMaxLecturesPerDay, 
                                                        Enums.TimetableType.Lectures)
                                            .WithValue("3");
        await _constraintRepository.AddAsync(maxLecturesPerDayConstraint, saveChanges: false);
    }

    private async Task AddAdminSettings()
    {
        var isInitialized = (await _adminSettingsRepository.GetAllAsync()).Count > 0;
        if (isInitialized) return;

        var keys = Enum.GetValues<AdminConfigurationKeys>();
        foreach (var key in keys)
        {
            switch (key)
            {
                case AdminConfigurationKeys.GeneralClassSizeLimit:
                    await _adminSettingsRepository.AddAsync(AdminSettings.Create(key, "80"), saveChanges: false);
                    break;
                case AdminConfigurationKeys.LectureTimeSlots:
                    await AddLectureTimeSlots(key);
                    break;
                case AdminConfigurationKeys.DaysOfWeek:
                    await AddDaysOfWeek(key);
                    break;
                default:
                    break;
            }
        }
    }

    private async Task AddLectureTimeSlots(AdminConfigurationKeys key)
    {
        var timeSlots = new List<string>()
        {
            "6am",
            "8am",
            "10am",
            "12:30pm",
            "2:30pm",
            "4:30pm"
        };
        await _adminSettingsRepository.AddAsync(AdminSettings.Create(key, 
            JsonSerializer.Serialize(timeSlots)), saveChanges: false);
    }
    
    private async Task AddDaysOfWeek(AdminConfigurationKeys key)
    {
        var daysOfWeek = new List<string>()
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday"
        };
        await _adminSettingsRepository.AddAsync(AdminSettings.Create(key, 
            JsonSerializer.Serialize(daysOfWeek)), saveChanges: false);
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