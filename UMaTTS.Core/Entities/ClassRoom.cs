namespace UMaTLMS.Core.Entities;

public class ClassRoom : Entity
{
    private ClassRoom() { }
    private ClassRoom(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
        IncludeInGeneralAssignment = true;
        IsExaminationCenter = true;
    }

    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public bool IsLab { get; private set; }
    public bool IsExaminationCenter { get; private set; }
    public bool IncludeInGeneralAssignment { get; private set; }
    public static ClassRoom Create(string name, int capacity)
        => new ClassRoom(name, capacity);

    public ClassRoom WithName(string name)
    {
        Name = name;
        return this;
    }

    public ClassRoom HasCapacity(int capacity)
    {
        Capacity = capacity;
        return this;
    }

    public ClassRoom IsLabRoom(bool isLab)
    {
        IsLab = isLab;
        if (isLab) IncludeInGeneralAssignment = false;
        return this;
    }

    public ClassRoom IsExcludedFromGeneralAssignment(bool value = false)
    {
        IncludeInGeneralAssignment = value;
        return this;
    }

    public ClassRoom IsNotExaminationCenter(bool value = false)
    {
        IsExaminationCenter = value;
        return this;
    }
}