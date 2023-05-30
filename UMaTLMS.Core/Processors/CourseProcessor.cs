using UMaTLMS.Core.Contracts;

namespace UMaTLMS.Core.Processors;

[Processor]
public class CourseProcessor
{
    private readonly ICourseRepository _courseRepository;

    public CourseProcessor(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(CourseCommand command)
    {
        var isNew = command.Id == 0;
        IncomingCourse? course;

        if (isNew)
        {
            course = IncomingCourse.Create(command.Name, command.Credit, command.YearGroup);
            course.HasCode(command.Code)
                .ForAcademicPeriod(command.AcademicPeriod)
                .ForYear(command.Year)
                .HasGroup(command.CourseGroup)
                .HasCategory(command.CourseCategory)
                .HasCategory(command.CourseCategory)
                .HasCourseId(command.CourseId)
                //.ForProgramme(command.ProgrammeId)
                .HasExaminers(command.FirstExaminerStaffId, command.SecondExaminerStaffId)
                .HasType(command.CourseType);
            await _courseRepository.AddAsync(course);
            return course.Id;
        }

        course = await _courseRepository.FindByIdAsync(command.Id);
        if (course is null) return new NullReferenceException();

        course.HasCode(command.Code)
            .ForAcademicPeriod(command.AcademicPeriod)
            .ForYear(command.Year)
            .HasGroup(command.CourseGroup)
            .HasCategory(command.CourseCategory)
            .HasCategory(command.CourseCategory)
            .HasCourseId(command.CourseId)
            //.ForProgramme(command.ProgrammeId)
            .HasExaminers(command.FirstExaminerStaffId, command.SecondExaminerStaffId)
            .HasType(command.CourseType);
        await _courseRepository.UpdateAsync(course);
        return course.Id;
    }

    public async Task<OneOf<CourseDto, Exception>> GetAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return new NullReferenceException();

        return course.Adapt<CourseDto>();
    }

    public async Task<PaginatedList<CourseDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _courseRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<CourseDto>>();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return;

        await _courseRepository.SoftDeleteAsync(course);
    }

    public async Task HardDeleteAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return;

        await _courseRepository.DeleteAsync(course);
    }
}

public record CourseCommand(int Id,  int YearGroup, string? Code, string Name, int Credit, int? Year, int CourseGroup,
    int CourseCategory, int CourseType, string? CourseId, int? ProgrammeId, int? FirstExaminerStaffId, 
    int? SecondExaminerStaffId, AcademicPeriodResponse AcademicPeriod);

public record CourseDto(int Id,  int? YearGroup, string? Code, string Name, int Credit, int? Year, int CourseGroup,
    int CourseCategory, int CourseType, string? CourseId, int? ProgrammeId, int? FirstExaminerStaffId, 
    int? SecondExaminerStaffId, AcademicPeriodResponse AcademicPeriod);