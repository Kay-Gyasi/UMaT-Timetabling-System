using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ClassProcessor
{
	private readonly IClassGroupRepository _classGroupRepository;
	private readonly ISubClassGroupRepository _subClassGroupRepository;

	public ClassProcessor(IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository)
    {
		_classGroupRepository = classGroupRepository;
		_subClassGroupRepository = subClassGroupRepository;
	}

	public async Task<OneOf<int, Exception>> CreateSub(SubClassGroupCommand command)
	{
		var isValid = await _subClassGroupRepository.IsValid(command.GroupId, command.Size, command.Name);
		if (!isValid) return new InvalidDataException();

		var subClass = SubClassGroup.Create(command.GroupId, command.Size, command.Name);
		await _subClassGroupRepository.AddAsync(subClass);
		return subClass.Id;
	}

	public async Task<ClassGroupDto?> Get(int classId)
	{
		var @class = await _classGroupRepository.FindByIdAsync(classId);
		if (@class == null) return null;

		return @class.Adapt<ClassGroupDto>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<SubClassGroupDto?> GetSub(int subClassId)
	{
		var sub = await _subClassGroupRepository.FindByIdAsync(subClassId);
		if (sub is null) return null;

		return sub.Adapt<SubClassGroupDto>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<PaginatedList<ClassGroupPageDto>?> GetPage(PaginatedCommand command)
	{
		var paginated = await _classGroupRepository.GetPageAsync(command);
		return paginated.Adapt<PaginatedList<ClassGroupPageDto>?>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<PaginatedList<SubClassGroupPageDto>?> GetSubPage(PaginatedCommand command)
	{
		var paginated = await _subClassGroupRepository.GetPageAsync(command);
		return paginated.Adapt<PaginatedList<SubClassGroupPageDto>?>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<bool> DeleteAllSubs(int classId)
	{
		var @class = await _classGroupRepository.FindByIdAsync(classId);
		if (@class == null) return false;

		foreach (var sub in @class.SubClassGroups)
		{
			await _subClassGroupRepository.DeleteAsync(sub, false).ConfigureAwait(false);
		}

		return await _subClassGroupRepository.SaveChanges();
	}

	public async Task<bool> DeleteSub(int subClassId) 
	{
		var sub = await _subClassGroupRepository.FindByIdAsync(subClassId);
		if (sub == null) return false;

		await _subClassGroupRepository.DeleteAsync(sub);
		return true;
	}
}

public record SubClassGroupCommand(int GroupId, int Size, string Name);
public record SubClassGroupDto(int Id, int GroupId, int Size, string Name, List<LectureDto> Lectures);
public record SubClassGroupPageDto(int Id, int GroupId, int Size, string Name);
public record ClassGroupDto(int Id, int UmatId, int Size, string Name, List<SubClassGroupDto> SubClassGroups);
public record ClassGroupPageDto(int Id, int UmatId, int Size, string Name);
