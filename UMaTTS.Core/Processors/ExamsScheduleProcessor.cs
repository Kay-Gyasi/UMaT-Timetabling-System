using FluentValidation;
using Microsoft.Extensions.Configuration;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ExamsScheduleProcessor
{
    private const string _timetableFile = "Timetable:ExamFile";
    private const string _timetableType = "Timetable:Type";
    private const string _timetableDownload = "Timetable:ExamDownloadName";
    private readonly ILogger<ExamsScheduleProcessor> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IConfiguration _configuration;
    private readonly IExcelReader _excelReader;
    private readonly ClassProcessor _classProcessor;

    public ExamsScheduleProcessor(ILogger<ExamsScheduleProcessor> logger, ICourseRepository courseRepository,
        ISubClassGroupRepository subClassGroupRepository, IRoomRepository roomRepository,
        ILectureRepository lectureRepository, ILecturerRepository lecturerRepository,
        IConfiguration configuration, IExcelReader excelReader, ClassProcessor classProcessor)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _roomRepository = roomRepository;
        _lectureRepository = lectureRepository;
        _lecturerRepository = lecturerRepository;
        _configuration = configuration;
        _excelReader = excelReader;
        _classProcessor = classProcessor;
    }

    public async Task<OneOf<bool, Exception>> Generate(ExamsScheduleCommand command)
    {
        var rooms = await _roomRepository.GetAllAsync(x => x.IsExaminationCenter);
        var lecturers = await _lecturerRepository.GetAllAsync();
        var groups = await _subClassGroupRepository.GetAllAsync();
        if (!groups.Any())
        {
            await _classProcessor.AddSubClassGroups();
            groups = await _subClassGroupRepository.GetAllAsync();
        }

        var courses = await _courseRepository.GetAllAsync(x => x.IsExaminable || x.HasPracticalExams);
        if (!rooms.Any()) return new EmptyRoomsDataException();
        if (!lecturers.Any()) return new EmptyLecturersDataException();
        if (!groups.Any()) return new EmptyGroupsDataException();
        if (!courses.Any()) return new EmptyCoursesDataException();

        var schedules = await Task.Run(() => ExamsTimetableGenerator.Generate(rooms, groups, lecturers, courses, command));
        if (!AllCoursesHaveBeenScheduled(schedules, courses)) return new ExamNotScheduledException();
        
        var fileName = _configuration[_timetableFile] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(fileName)) return new TimetableGeneratedException();
        if (File.Exists(fileName)) return new TimetableGeneratedException();

        await ExamsTimetableGenerator.GetAsync(_excelReader, schedules, fileName, rooms);
        return true;
    }

    private static bool AllCoursesHaveBeenScheduled(List<List<ExamsSchedule>> schedules, List<IncomingCourse> courses)
    {
        var unscheduledCourses = new List<IncomingCourse>();
        foreach (var course in courses)
        {
            var courseHasBeenScheduled = schedules.Any(x => x.Any(a => a.CourseName == course.Name));
            if (!courseHasBeenScheduled) unscheduledCourses.Add(course);
        }

        return unscheduledCourses.Count == 0;
    }
}

public record ExamsScheduleCommand(DateTime StartDate, DateTime EndDate, bool IncludeSaturdays,
    bool IncludeSundays, DateTime PracticalsStartDate, DateTime PracticalsEndDate);

public class ExamsScheduleCommandValidator : AbstractValidator<ExamsScheduleCommand>
{
    public ExamsScheduleCommandValidator()
    {
        RuleFor(x => x.StartDate.Date)
            .GreaterThanOrEqualTo(DateTime.Now.Date);
        RuleFor(x => x.EndDate.Date)
            .GreaterThanOrEqualTo(x => x.StartDate.Date);
        RuleFor(x => x.PracticalsStartDate.Date)
            .GreaterThanOrEqualTo(DateTime.Now.Date);
        RuleFor(x => x.PracticalsEndDate.Date)
            .GreaterThanOrEqualTo(x => x.PracticalsStartDate.Date);
    }
}