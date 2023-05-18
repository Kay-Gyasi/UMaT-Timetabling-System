namespace UMaTLMS.Core.Processors;

[Processor]
public class DepartmentProcessor
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentProcessor(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(DepartmentCommand command)
    {
        var isNew = command.Id is null;
        Department? department;

        if (isNew)
        {
            department = Department.Create(command.Name, command.Code);
            await _departmentRepository.AddAsync(department);
            return department.Id;
        }

        department = await _departmentRepository.FindByIdAsync(command.Id!.Value);
        if (department is null) return new NullReferenceException();

        department.HasName(command.Name)
            .HasCode(command.Code);
        await _departmentRepository.UpdateAsync(department);
        return department.Id;
    }

    public async Task<OneOf<DepartmentDto, Exception>> GetAsync(int id)
    {
        var department = await _departmentRepository.FindByIdAsync(id);
        if (department is null) return new NullReferenceException();

        return department.Adapt<DepartmentDto>();
    }

    public async Task<PaginatedList<DepartmentPageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _departmentRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<DepartmentPageDto>>();
    }

    public async Task<bool> Exists(string code)
    {
        return await _departmentRepository.Exists(code);
    }


    public async Task DeleteAsync(int id)
    {
        var department = await _departmentRepository.FindByIdAsync(id);
        if (department is null) return;

        await _departmentRepository.SoftDeleteAsync(department);
    }

    public async Task HardDeleteAsync(int id)
    {
        var department = await _departmentRepository.FindByIdAsync(id);
        if (department is null) return;

        await _departmentRepository.DeleteAsync(department);
    }
}

public record DepartmentCommand(int? Id, string Name, string Code);
public record DepartmentDto(int Id, string Name, string Code, IEnumerable<LecturerDto> Lectures,
    IEnumerable<CourseDto> Courses, IEnumerable<Class> Classes);
public record DepartmentPageDto(int Id, string Name, string Code);
