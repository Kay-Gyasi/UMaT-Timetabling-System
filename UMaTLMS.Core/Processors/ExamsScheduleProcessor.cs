using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ExamsScheduleProcessor
{
    private readonly IExamsScheduleRepository _examsScheduleRepository;
    private readonly ILogger<ExamsScheduleProcessor> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureRepository _lectureRepository;

    public ExamsScheduleProcessor(IExamsScheduleRepository examsScheduleRepository, 
        ILogger<ExamsScheduleProcessor> logger, ICourseRepository courseRepository,
        ISubClassGroupRepository subClassGroupRepository, IRoomRepository roomRepository,
        ILectureRepository lectureRepository)
    {
        _examsScheduleRepository = examsScheduleRepository;
        _logger = logger;
        _courseRepository = courseRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _roomRepository = roomRepository;
        _lectureRepository = lectureRepository;
    }

    public async Task<OneOf<bool, Exception>> Generate(ExamsScheduleCommand command)
    {
        var rooms = await _roomRepository.GetAll();
        var lectures = await _lectureRepository.GetAll();
        var groups = await _subClassGroupRepository.GetAll();

        var schedules = await ExamsTimetableGenerator.Generate(lectures, rooms, groups, command);
        foreach (var schedule in schedules)
        {
            await _examsScheduleRepository.UpdateAsync(schedule, saveChanges: false).ConfigureAwait(false);
        }
        
        var isSaved = await _examsScheduleRepository.SaveChanges();
        if (!isSaved) return new InvalidDataException();

        var isTimetableBuilt = await ExamsTimetableGenerator.GenerateTimetable(schedules);
        if (!isTimetableBuilt) return new InvalidDataException();
        return true;
    }
}

public record ExamsScheduleCommand(DateTime StartDate, DateTime EndDate, bool IncludeSaturdays,
    bool IncludeSundays, DateTime PracticalsStartDate, DateTime PracticalsEndDate);