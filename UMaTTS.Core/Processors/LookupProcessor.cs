namespace UMaTLMS.Core.Processors;

[Processor]
public class LookupProcessor
{
    private readonly IRoomRepository _roomRepository;

    public LookupProcessor(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<Lookup>> GetAsync(LookupType type)
    {
        return type switch
        {
            0 => await _roomRepository.GetLookup(),
            _ => throw new NotImplementedException()
        };
    }
}

public enum LookupType
{
    Rooms
}
