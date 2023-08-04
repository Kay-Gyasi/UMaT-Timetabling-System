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
        IsExaminable = name.ToLower().Contains("field trip") == false;
        Credit = credit;
    }

    public int? UmatId { get; private set; }
    public int? YearGroup { get; private set; }
    public AcademicPeriodResponse AcademicPeriod { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public int Credit { get; private set; }
    public int TeachingHours { get; private set; }
    public int PracticalHours { get; private set; }
    public int? Year { get; private set; }
    public int CourseGroup { get; private set; }
    public int CourseCategory { get; private set; }
    public int CourseType { get; private set; }
    public string? CourseId { get; private set; }
    public int? ProgrammeId { get; private set; }
    public string? ProgrammeCode { get; private set; }
    public int? FirstExaminerStaffId { get; private set; }
    public int? SecondExaminerStaffId { get; private set; }
    public bool IsExaminable { get; private set; } = true;
    public bool IsToHaveWeeklyLectureSchedule { get; private set; } = true;
    public bool HasPracticalExams { get; private set; }
    private List<Preference> _preferences = new();
    public IReadOnlyList<Preference> Preferences => _preferences.AsReadOnly();

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

    public IncomingCourse MarkAsNotExaminable(bool isExaminable = false)
    {
        IsExaminable = isExaminable;
        return this;
    }

    public IncomingCourse MarkAsHavingPracticalExams(bool hasPracticals = true)
    {
        HasPracticalExams = hasPracticals;
        return this;
    }

    public IncomingCourse HasNoWeeklyLectures(bool hasWeeklyLectures = false)
    {
        IsToHaveWeeklyLectureSchedule = hasWeeklyLectures;
        return this;
    }

    public IncomingCourse WithHours(int? teachingHours, int? practicalHours)
    {
        if (teachingHours is null && practicalHours is null)
        {
            switch (Credit)
            {
                case 3:
                    teachingHours = 2;
                    practicalHours = 2;
                    break;
                case 2:
                    teachingHours = 2;
                    practicalHours = 1;
                    break;
                case 1:
                    teachingHours = 1;
                    practicalHours = 1;
                    break;
                default:
                    break;
            }
        }

        TeachingHours = teachingHours ?? 0;
        PracticalHours = practicalHours ?? 0;
        return this;
    }
}