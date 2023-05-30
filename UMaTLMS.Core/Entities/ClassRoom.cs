namespace UMaTLMS.Core.Entities;

public class ClassRoom : Entity
{
    private ClassRoom() { }
    private ClassRoom(string name, int? capacity)
    {
        Name = name;
        Capacity = capacity;
    }

    public string Name { get; private set; }
    public int? Capacity { get; private set; }
    public bool IsLab { get; private set; }
    public bool IsWorkshop { get; private set; }

    public static ClassRoom Create(string name, int? capacity)
        => new ClassRoom(name, capacity);

    public ClassRoom WithName(string name)
    {
        Name = name;
        return this;
    }

    public ClassRoom HasCapacity(int? capacity)
    {
        Capacity = capacity;
        return this;
    }

    public ClassRoom IsLabRoom()
    {
        IsLab = true;
        return this;
    }

    public ClassRoom IsWorkshopRoom()
    {
        IsWorkshop = true;
        return this;
    }
}