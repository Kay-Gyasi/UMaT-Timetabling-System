using System.Text;
using UMaTLMS.Core.Contracts;

namespace UMaTLMS.Core.Entities;

public class IncomingCourse : Entity
{
    private IncomingCourse() { }

    private IncomingCourse(string name, int credit, int? yearGroup, int? umatId)
    {
        UmatId = umatId;
        YearGroup = yearGroup;
        if (name.TrimEnd().EndsWith(" 1"))
        {
            var builder = new StringBuilder();
            builder.Append(name[..^2]);
            builder.Append(" I");
            name = builder.ToString();
        }
        Name = name;
        Credit = credit;
    }

    public int? UmatId { get; private set; }
    public int? YearGroup { get; private set; }
    public AcademicPeriodResponse AcademicPeriod { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public int Credit { get; private set; }
    public int? Year { get; private set; }
    public int CourseGroup { get; private set; }
    public int CourseCategory { get; private set; }
    public int CourseType { get; private set; }
    public string? CourseId { get; private set; }
    public int? ProgrammeId { get; private set; }
    public string? ProgrammeCode { get; private set; }
    public int? FirstExaminerStaffId { get; private set; }
    public int? SecondExaminerStaffId { get; private set; }

    public static IncomingCourse Create(string name, int credit, int? yearGroup, int? umatId = null) 
        => new(name, credit, yearGroup, umatId);

    public IncomingCourse ForAcademicPeriod(AcademicPeriodResponse academicPeriod)
    {
        AcademicPeriod = academicPeriod;
        return this;
    }

    public IncomingCourse HasCode(string? code)
    {
        Code = code;
        return this;
    }

    public IncomingCourse ForYear(int? year)
    {
        Year = year;
        return this;
    }

    public IncomingCourse HasGroup(int courseGroup)
    {
        CourseGroup = courseGroup;
        return this;
    }
    
    public IncomingCourse HasCategory(int courseCategory)
    {
        CourseCategory = courseCategory;
        return this;
    }
    
    public IncomingCourse HasType(int courseType)
    {
        CourseType = courseType;
        return this;
    }

    public IncomingCourse HasCourseId(string? courseId)
    {
        CourseId = courseId;
        return this;
    }

    public IncomingCourse ForProgramme(int? programmeId, string? code)
    {
        ProgrammeId = programmeId;
        ProgrammeCode = code;
        return this;
    }

    public IncomingCourse HasExaminers(int? first, int? second)
    {
        FirstExaminerStaffId = first;
        SecondExaminerStaffId = second;
        return this;
    }
}