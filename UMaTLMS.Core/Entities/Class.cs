namespace UMaTLMS.Core.Entities;

public class Class : Entity
{
    private Class(Level level, int departmentId)
    {
        Level = level;
        DepartmentId = departmentId;
    }

    public int DepartmentId { get; private set; }
    public Level Level { get; private set; }
    public int Size { get; private set; }
    public Department? Department { get; private set; }

    private readonly List<Course> _courses = new();
    public IEnumerable<Course> Courses => _courses.AsReadOnly();

    public static Class Create(Level level, int departmentId)
        => new Class(level, departmentId);

    public Class InDepartment(int departmentId)
    {
        DepartmentId = departmentId;
        return this;
    }

    public Class AtLevel(Level level)
    {
        Level = level;
        return this;
    }
}

public enum Level
{
    One = 100,
    Two = 200,
    Three = 300,
    Four = 400
}