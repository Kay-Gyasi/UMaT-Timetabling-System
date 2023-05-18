namespace UMaTLMS.Core.Entities;

public class Department : Entity
{
    private Department(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }

    private readonly List<Lecturer> _lecturers = new();
    public IEnumerable<Lecturer> Lectures => _lecturers.AsReadOnly();

    private readonly List<Course> _courses = new();
    public IEnumerable<Course> Courses => _courses.AsReadOnly();

    private readonly List<Class> _classes = new();
    public IEnumerable<Class> Classes => _classes.AsReadOnly();

    public static Department Create(string name, string code)
        => new Department(name, code);

    public Department HasName(string name)
    {
        Name = name;
        return this;
    }

    public Department HasCode(string code)
    {
        Code = code;
        return this;
    }
}