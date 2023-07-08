namespace UMaTLMS.Core.Entities;

public class Lecturer : Entity
{
    private Lecturer(int umatId, string? name)
    {
        UmatId = umatId;
        Name = name;
    }

    public int UmatId { get; private set; }
    public string? Name { get; private set; }
    private List<ExamsSchedule> _examsSchedules = new();
    public IReadOnlyList<ExamsSchedule> ExamsSchedules => _examsSchedules.AsReadOnly();

    public static Lecturer Create(int umatId, string? name) => new(umatId, name);

    public Lecturer HasUmatId(int umatId)
    {
        UmatId = umatId;
        return this;
    }
}