namespace UMaTLMS.Core.Processors;

[Processor]
public class LookupProcessor
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILecturerRepository _lecturerRepository;

    public LookupProcessor(IRoomRepository roomRepository, ILecturerRepository lecturerRepository)
    {
        _roomRepository = roomRepository;
        _lecturerRepository = lecturerRepository;
    }

    public async Task<List<Lookup>> GetAsync(LookupType type)
    {
        return type switch
        {
            LookupType.Rooms => await _roomRepository.GetLookup(),
            LookupType.Lecturers => await _lecturerRepository.GetLookup(),
            _ => throw new NotImplementedException()
        };
    }
}

public enum LookupType
{
    Rooms,
    Lecturers
}
