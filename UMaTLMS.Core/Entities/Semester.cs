namespace UMaTLMS.Core.Entities;

public class Semester : Entity
{
    private Semester(DateTime startDate, DateTime endDate, SemesterType? type)
    {
        StartDate = startDate;
        EndDate = endDate;
        Type = type;
    }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public SemesterType? Type { get; private set; }

    private readonly List<Course> _courses = new();
    public IEnumerable<Course> Courses => _courses.AsReadOnly();

    public static Semester Create(DateTime startDate, DateTime endDate, SemesterType? type)
        => new Semester(startDate, endDate, type);

    public Semester StartsOn(DateTime startDate)
    {
        StartDate = startDate;
        return this;
    }

    public Semester EndsOn(DateTime endDate)
    {
        EndDate = endDate;
        return this;
    }
}

public enum SemesterType
{
    First = 1,
    Second
}