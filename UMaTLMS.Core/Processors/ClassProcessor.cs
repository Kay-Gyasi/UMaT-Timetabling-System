namespace UMaTLMS.Core.Processors;

[Processor]
public class ClassProcessor
{
    private readonly IClassRepository _classRepository;

    public ClassProcessor(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(ClassCommand command)
    {
        var isNew = command.Id is null;
        Class? @class;

        if (isNew)
        {
            @class = Class.Create(command.Level, command.DepartmentId);
            await _classRepository.AddAsync(@class);
            return @class.Id;
        }

        @class = await _classRepository.FindByIdAsync(command.Id!.Value);
        if (@class is null) return new NullReferenceException();

        @class.InDepartment(command.DepartmentId)
            .AtLevel(command.Level);
        await _classRepository.UpdateAsync(@class);
        return @class.Id;
    }

    public async Task<OneOf<ClassDto, Exception>> GetAsync(int id)
    {
        var @class = await _classRepository.FindByIdAsync(id);
        if (@class is null) return new NullReferenceException();

        return @class.Adapt<ClassDto>();
    }

    public async Task<PaginatedList<ClassPageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _classRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<ClassPageDto>>();
    }

    public async Task DeleteAsync(int id)
    {
        var @class = await _classRepository.FindByIdAsync(id);
        if (@class is null) return;
        await _classRepository.SoftDeleteAsync(@class);
    }

    public async Task HardDeleteAsync(int id)
    {
        var @class = await _classRepository.FindByIdAsync(id);
        if (@class is null) return;
        await _classRepository.DeleteAsync(@class);
    }
}

public record ClassCommand(int? Id, int DepartmentId, Level Level);

public record ClassDto(int Id, int DepartmentId, Level Level, DepartmentDto Department, IEnumerable<CourseDto> Courses);

public record ClassPageDto(int Id, int DepartmentId, Level Level, DepartmentPageDto Department);
