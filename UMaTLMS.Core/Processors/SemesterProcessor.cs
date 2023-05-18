namespace UMaTLMS.Core.Processors;

[Processor]
public class SemesterProcessor
{
    private readonly ISemesterRepository _semesterRepository;

    public SemesterProcessor(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(SemesterCommand command)
    {
        var isNew = command.Id is null;
        Semester? semester;

        if (isNew)
        {
            semester = Semester.Create(command.StartDate, command.EndDate, null);
            await _semesterRepository.AddAsync(semester);
            return semester.Id;
        }

        semester = await _semesterRepository.FindByIdAsync(command.Id!.Value);
        if (semester is null) return new NullReferenceException();

        semester.StartsOn(command.StartDate)
            .EndsOn(command.EndDate);
        await _semesterRepository.UpdateAsync(semester);
        return semester.Id;
    }

    public async Task<OneOf<ClassDto, Exception>> GetAsync(int id)
    {
        var semester = await _semesterRepository.FindByIdAsync(id);
        if (semester is null) return new NullReferenceException();

        return semester.Adapt<ClassDto>();
    }

    public async Task<PaginatedList<ClassPageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _semesterRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<ClassPageDto>>();
    }

    public async Task DeleteAsync(int id)
    {
        var semester = await _semesterRepository.FindByIdAsync(id);
        if (semester is null) return;

        await _semesterRepository.SoftDeleteAsync(semester);
    }

    public async Task HardDeleteAsync(int id)
    {
        var semester = await _semesterRepository.FindByIdAsync(id);
        if (semester is null) return;

        await _semesterRepository.DeleteAsync(semester);
    }
}

public record SemesterCommand(int? Id, DateTime StartDate, DateTime EndDate);
public record SemesterDto(int Id, DateTime StartDate, DateTime EndDate, SemesterType Type);
public record SemesterPageDto(int Id, DateTime StartDate, DateTime EndDate, SemesterType Type);