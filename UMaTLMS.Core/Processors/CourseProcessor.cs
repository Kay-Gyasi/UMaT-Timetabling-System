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
        var isNew = command.Id is null;
        Course? course;

        if (isNew)
        {
            course = Course.Create(command.Name, command.Code);
            course.WithDescription(command.Description)
                .BelongsTo(command.DepartmentId)
                .HasCreditHours(command.CreditHours)
                .ForSemester(command.SemesterId);
            await _courseRepository.AddAsync(course);
            return course.Id;
        }

        course = await _courseRepository.FindByIdAsync(command.Id!.Value);
        if (course is null) return new NullReferenceException();

        course.WithName(command.Name)
            .WithCode(command.Code)
            .WithDescription(command.Description)
            .BelongsTo(command.DepartmentId)
            .HasCreditHours(command.CreditHours)
            .ForSemester(command.SemesterId);
        await _courseRepository.UpdateAsync(course);
        return course.Id;
    }

    public async Task<OneOf<CourseDto, Exception>> GetAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return new NullReferenceException();

        return course.Adapt<CourseDto>();
    }

    public async Task<PaginatedList<CoursePageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _courseRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<CoursePageDto>>();
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

public record CourseCommand(int? Id, int DepartmentId, int SemesterId, int CreditHours, string Name, string Code,
    string? Description);

public record CourseDto(int Id, int DepartmentId, int CreditHours, int SemesterId, string Name, string Code,
    string? Description, DepartmentDto? Department, IEnumerable<ClassDto> Classes);

public record CoursePageDto(int Id, int DepartmentId, int SemesterId, int CreditHours, string Name, string Code,
    string? Description, DepartmentDto? Department);
