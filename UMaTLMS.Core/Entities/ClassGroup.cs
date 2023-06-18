namespace UMaTLMS.Core.Entities;

public class ClassGroup : Entity
{
    private ClassGroup(int umatId, int? size, string name)
    {
        Size = size;
        UmatId = umatId;
        Name = name;
        NumOfSubClasses = 1;
    }

    public int UmatId { get; private set; }
    public int? Size { get; private set; }
    public int NumOfSubClasses { get; private set; }
    public int Year => GetYear();
    public string Name { get; private set; }
    private List<SubClassGroup> _subClassGroups = new();
    public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();

    public static ClassGroup Create(int umatId, int? size, string name) 
        => new(umatId, size, name);

    public ClassGroup HasNoOfSubClasses(int number)
    {
        NumOfSubClasses = number;
        return this;
    }

    private int GetYear()
    {
        return Name.Last() switch
        {
            '1' => 100,
            '2' => 200,
            '3' => 300,
            '4' => 400,
            _ => 0
        };
    }
}