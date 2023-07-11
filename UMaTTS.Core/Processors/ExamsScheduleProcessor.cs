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
    private readonly IExamsScheduleRepository _examsScheduleRepository;
    private readonly ILogger<ExamsScheduleProcessor> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IConfiguration _configuration;
    private readonly IExcelReader _excelReader;

    public ExamsScheduleProcessor(IExamsScheduleRepository examsScheduleRepository, 
        ILogger<ExamsScheduleProcessor> logger, ICourseRepository courseRepository,
        ISubClassGroupRepository subClassGroupRepository, IRoomRepository roomRepository,
        ILectureRepository lectureRepository, ILecturerRepository lecturerRepository,
        IConfiguration configuration, IExcelReader excelReader)
    {
        _examsScheduleRepository = examsScheduleRepository;
        _logger = logger;
        _courseRepository = courseRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _roomRepository = roomRepository;
        _lectureRepository = lectureRepository;
        _lecturerRepository = lecturerRepository;
        _configuration = configuration;
        _excelReader = excelReader;
    }

    public async Task<OneOf<bool, Exception>> Generate(ExamsScheduleCommand command)
    {
        var rooms = await _roomRepository.GetAllAsync(x => x.IsExaminationCenter);
        var lectures = await _lectureRepository.GetAllAsync(x => x.Course!.IsExaminable || x.Course!.HasPracticalExams);
        var lecturers = await _lecturerRepository.GetAllAsync();
        var groups = await _subClassGroupRepository.GetAllAsync();
        var courses = await _courseRepository.GetAllAsync();

        var schedules = await Task.Run(() =>
            ExamsTimetableGenerator.Generate(lectures, rooms, groups, lecturers, courses, command));
        
        var fileName = _configuration[_timetableFile] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(fileName)) return new TimetableGeneratedException();
        if (File.Exists(fileName)) return new TimetableGeneratedException();

        await ExamsTimetableGenerator.GetAsync(_excelReader, schedules, fileName);
        return true;
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