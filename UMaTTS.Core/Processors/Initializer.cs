using OfficeOpenXml;
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
    private ExcelWorksheet _firstSemesterWorksheet;
    private const string _firstSemFile = "_content/DRAFT TIME TABLE SEM ONE 2022_2023.xlsx";

    public Initializer(IExcelReader reader, IRoomRepository roomRepository, 
        ILectureScheduleRepository lectureScheduleRepository, IOnlineLectureScheduleRepository onlineLectureScheduleRepository, 
        ILectureRepository lectureRepository, ICourseRepository courseRepository, ILecturerRepository  lecturerRepository, 
        IConstraintRepository constraintRepository,IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository,
        IPreferenceRepository preferenceRepository)
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
    }

    public async Task Reset()
    {
        try
        {
            var lectureSchedules = await _lectureScheduleRepository.GetAllAsync();
            await _lectureScheduleRepository.DeleteAllAsync(lectureSchedules, saveChanges: false);

            var onlineSchedules = await _onlineLectureScheduleRepository.GetAllAsync();
            await _onlineLectureScheduleRepository.DeleteAllAsync(onlineSchedules, saveChanges: false);

            var preferences = await _preferenceRepository.GetAllAsync();
            await _preferenceRepository.DeleteAllAsync(preferences, saveChanges: false);

            var constraints = await _constraintRepository.GetAllAsync();
            await _constraintRepository.DeleteAllAsync(constraints, saveChanges: false);

            var subGroups = await _subClassGroupRepository.GetAllAsync();
            await _subClassGroupRepository.DeleteAllAsync(subGroups, saveChanges: false);

            var groups = await _classGroupRepository.GetAllAsync();
            await _classGroupRepository.DeleteAllAsync(groups, saveChanges: false);

            var courses = await _courseRepository.GetAllAsync();
            await _courseRepository.DeleteAllAsync(courses, saveChanges: false);

            var lecturers = await _lecturerRepository.GetAllAsync();
            await _lecturerRepository.DeleteAllAsync(lecturers, saveChanges: false);

            var lectures = await _lectureRepository.GetAllAsync();
            await _lectureRepository.DeleteAllAsync(lectures, saveChanges: false);

            await _classGroupRepository.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task AddBaseConstraints()
    {
        var isInitialized = await _constraintRepository.IsInitialized();
        if (isInitialized) return;

        var maxLecturesPerDayConstraint = Constraint.Create(ConstraintType.GeneralMaxLecturesPerDay, Enums.TimetableType.Lectures)
                                    .WithValue("4");
        await _constraintRepository.AddAsync(maxLecturesPerDayConstraint);
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