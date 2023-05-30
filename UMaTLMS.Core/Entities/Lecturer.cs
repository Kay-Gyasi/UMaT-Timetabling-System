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

    public static Lecturer Create(int umatId, string? name) => new(umatId, name);
}