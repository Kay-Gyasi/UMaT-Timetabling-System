namespace UMaTLMS.Core.Contracts;

public class CourseResponse
{
    public int Id { get; set; }
    public int? YearGroup { get; set; }
    public AcademicPeriodResponse AcademicPeriod { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int Credit { get; set; }
    public int? Year { get; set; }
    public int CourseGroup { get; set; }
    public int CourseCategory { get; set; }
    public int CourseType { get; set; }
    public string? CourseId { get; set; }
    public Programme? Programme { get; set; }
    public Staff? FirstExaminerStaff { get; set; }
    public Staff? SecondExaminerStaff { get; set; }
}

public class Programme
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    // public string? Duration { get; set; }
    public string? IndexNumberPrefix { get; set; }
    public string? Description { get; set; }
    public int ProgrammeCourseRegistrationType { get; set; }
    public Certificate Certificate { get; set; }
    public Department Department { get; set; }
}

public class Certificate
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
}

public class Department
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Faculty Faculty { get; set; }
}

public class Faculty
{
    public string Code { get; set; }
    public string? Email { get; set; }
    public int Index { get; set; }
    public SchoolCentre SchoolCentre { get; set; }
}

public class SchoolCentre
{
    public int Id { get; set; }
    public Campus Campus { get; set; }
}

public class Campus
{
    public int Id { get; set; }
    public string Name { get; set; }
}