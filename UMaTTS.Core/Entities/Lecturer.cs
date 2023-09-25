namespace UMaTLMS.Core.Entities;

public class Lecturer : Entity
{
    private Lecturer(int umatId, string? name, string? titledName)
    {
        UmatId = umatId;
        Name = name;
        TitledName = titledName;
    }

    public int UmatId { get; private set; }
    public string? Name { get; private set; }
    public string? TitledName { get; private set; }

    private List<ExamsSchedule> _examsSchedules = new();
    public IReadOnlyList<ExamsSchedule> ExamsSchedules => _examsSchedules.AsReadOnly();

    private List<Preference> _preferences = new();
    public IReadOnlyList<Preference> Preferences => _preferences.AsReadOnly();
    
    private List<Constraint> _constraints = new();
    public IReadOnlyList<Constraint> Constraints => _constraints.AsReadOnly();

    public static Lecturer Create(int umatId, string? name, string? titledName) => new(umatId, name, titledName);

    public Lecturer HasUmatId(int umatId)
    {
        UmatId = umatId;
        return this;
    }

    public Lecturer HasName(string name, string? titledName)
    {
        Name = name;
        TitledName = titledName ?? name;
        return this;
    }
}