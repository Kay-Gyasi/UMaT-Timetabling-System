namespace UMaTLMS.Core.Processors;

[Processor]
public class LookupProcessor
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly ICourseRepository _courseRepository;

    public LookupProcessor(IRoomRepository roomRepository, ILecturerRepository lecturerRepository,
        ICourseRepository courseRepository)
    {
        _roomRepository = roomRepository;
        _lecturerRepository = lecturerRepository;
        _courseRepository = courseRepository;
    }

    public async Task<List<Lookup>> GetAsync(LookupType type)
    {
        return type switch
        {
            LookupType.Rooms => await _roomRepository.GetLookup(),
            LookupType.Lecturers => await _lecturerRepository.GetLookup(),
            LookupType.Courses => (await _courseRepository.GetLookup()).DistinctBy(x => x.Name).ToList(),
            _ => throw new NotImplementedException()
        };
    }
}

public enum LookupType
{
    Rooms,
    Lecturers,
    Courses
}
