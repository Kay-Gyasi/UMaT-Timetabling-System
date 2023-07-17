using Microsoft.Extensions.Caching.Memory;
using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ClassProcessor
{
	private readonly IClassGroupRepository _classGroupRepository;
	private readonly ISubClassGroupRepository _subClassGroupRepository;
	private readonly ILogger<ClassProcessor> _logger;

    public ClassProcessor(IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository,
		ILogger<ClassProcessor> logger, IMemoryCache cache)
	{
		_classGroupRepository = classGroupRepository;
		_subClassGroupRepository = subClassGroupRepository;
		_logger = logger;
    }

	public async Task<OneOf<bool, Exception>> SetLimit(int limit)
	{
		var groupsInDb = await _classGroupRepository.GetAll();

		try
		{
			foreach (var group in groupsInDb)
			{
				if (group.Size is null) group.HasSize(0);
				var newNumberOfSubClasses = ((group.Size / limit) + 1) ?? 0;
				if (newNumberOfSubClasses == group.NumOfSubClasses) continue;

				group.HasNoOfSubClasses(newNumberOfSubClasses);
				await _classGroupRepository.UpdateAsync(group, saveChanges: false);

				await _subClassGroupRepository.DeleteAllAsync(group.SubClassGroups.ToList(), saveChanges: false);

				var sizes = group.Size.GetSubClassSizes(newNumberOfSubClasses);
				for (int i =  0; i < newNumberOfSubClasses; i++)
				{
					var subName = newNumberOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i + 1)}" : group.Name;
					await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i], subName), saveChanges: false);
				}
			}

			return await _classGroupRepository.SaveChanges();
		}
		catch (Exception e)
		{
			_logger.LogError("Error while adjusting class sizes. Message: {Message}", e.Message);
			return e;
		}
	}

	public async Task<OneOf<bool, Exception>> SetNumberOfSubClasses(int numberOfSubClasses, int classGroupId)
	{
		var @class = await _classGroupRepository.FindByIdAsync(classGroupId);
		if (@class is null) return new InvalidIdException();

		@class.HasNoOfSubClasses(numberOfSubClasses);
		await _classGroupRepository.UpdateAsync(@class);
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
