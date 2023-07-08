namespace UMaTLMS.Core.Entities;

public class SubClassGroup : Entity
{
    private SubClassGroup(int groupId, int? size, string name)
    {
        Size = size;
        GroupId = groupId;
        Name = name;
    }

    public int GroupId { get; private set; }
    public ClassGroup Group { get; private set; }
    public int? Size { get; private set; }
    public string Name { get; private set; }
    private List<Lecture> _lectures = new();
    public IReadOnlyList<Lecture> Lectures => _lectures.AsReadOnly();
    private List<ExamsSchedule> _examsSchedules = new();
    public IReadOnlyList<ExamsSchedule> ExamsSchedules => _examsSchedules.AsReadOnly();

    public static SubClassGroup Create(int groupId, int? size, string name) 
        => new(groupId, size, name);
}