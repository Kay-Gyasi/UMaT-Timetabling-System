namespace UMaTLMS.Core.Entities;

public class ClassGroup : Entity
{
    private ClassGroup(int umatId, int size, string name)
    {
        Size = size;
        UmatId = umatId;
        Name = name;
    }

    public int UmatId { get; private set; }
    public int Size { get; private set; }
    public string Name { get; private set; }
    private List<SubClassGroup> _subClassGroups = new();
    public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();

    public static ClassGroup Create(int umatId, int size, string name) 
        => new(umatId, size, name);
}