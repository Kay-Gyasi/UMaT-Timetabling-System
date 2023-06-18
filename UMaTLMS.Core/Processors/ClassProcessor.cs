using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ClassProcessor
{
	private readonly IClassGroupRepository _classGroupRepository;

	public ClassProcessor(IClassGroupRepository classGroupRepository)
    {
		_classGroupRepository = classGroupRepository;
	}

	public async Task<OneOf<bool, Exception>> SetNumberOfSubClasses(int numberOfSubClasses, int classGroupId)
	{
		var @class = await _classGroupRepository.FindByIdAsync(classGroupId);
		if (@class is null) return new InvalidIdException();

		@class.HasNoOfSubClasses(numberOfSubClasses);
		await _classGroupRepository.UpdateAsync(@class);
		await _classGroupRepository.SaveChanges();
		return true;
	}

	public async Task<OneOf<ClassGroupDto?, Exception>> GetAsync(int classId)
	{
		var @class = await _classGroupRepository.FindByIdAsync(classId);
		if (@class == null) return new InvalidIdException();

		return @class.Adapt<ClassGroupDto>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<PaginatedList<ClassGroupPageDto>> GetPageAsync(PaginatedCommand command)
	{
		var paginated = await _classGroupRepository.GetPageAsync(command);
		var result = paginated.Adapt<PaginatedList<ClassGroupPageDto>>(Mapping.GetTypeAdapterConfig());
        result.HasData(result.Data.OrderBy(x => x.Name).ToList());
        return result;
    }
}

public record SubClassGroupCommand(int? Id, int GroupId, int? Size, string Name);
public record SubClassGroupDto(int Id, int GroupId, int? Size, string Name, List<LectureDto> Lectures);
public record SubClassGroupPageDto(int Id, int GroupId, int? Size, string Name);
public record ClassGroupDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name, List<SubClassGroupDto> SubClassGroups);
public record ClassGroupPageDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name);
