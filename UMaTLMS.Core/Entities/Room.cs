namespace UMaTLMS.Core.Entities;

public class Room : Entity
{
    // ID counter used to assign IDs automatically
    private static int _nextRoomId = 0;
    private Room() { }
    private Room(string name, int? capacity)
    {
        Id = _nextRoomId++; // remove when using db entries
        Name = name;
        Capacity = capacity;
    }

    public string Name { get; private set; }
    public int? Capacity { get; private set; }
    public bool IsLab { get; private set; }
    public bool IsWorkshop { get; private set; }

    public static Room Create(string name, int? capacity)
        => new Room(name, capacity);

    public Room WithName(string name)
    {
        Name = name;
        return this;
    }

    public Room HasCapacity(int? capacity)
    {
        Capacity = capacity;
        return this;
    }

    public Room IsLabRoom()
    {
        IsLab = true;
        return this;
    }

    public Room IsWorkshopRoom()
    {
        IsWorkshop = true;
        return this;
    }

    // Restarts ID assigments
    public static void RestartIDs() { _nextRoomId = 0; }
}