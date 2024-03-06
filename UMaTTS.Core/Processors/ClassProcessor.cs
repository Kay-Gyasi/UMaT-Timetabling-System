using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class ClassProcessor
{
	private readonly IClassGroupRepository _classGroupRepository;
	private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IAdminSettingsRepository _adminSettingsRepository;
    private readonly ILogger<ClassProcessor> _logger;

    public ClassProcessor(IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository,
		IAdminSettingsRepository adminSettingsRepository, ILogger<ClassProcessor> logger)
	{
		_classGroupRepository = classGroupRepository;
		_subClassGroupRepository = subClassGroupRepository;
        _adminSettingsRepository = adminSettingsRepository;
        _logger = logger;
    }

	public async Task<OneOf<bool, Exception>> SetLimit(int limit)
	{
		var groupsInDb = await _classGroupRepository.GetAllAsync();
		var subClasses = await _subClassGroupRepository.GetAllAsync();
		await _subClassGroupRepository.DeleteAllAsync(subClasses, saveChanges: false);

		try
		{
			foreach (var group in groupsInDb)
			{
				if (group.Size is null) group.HasSize(0);
				var newNumberOfSubClasses = ((group.Size / limit) + 1) ?? 0;
				//if (newNumberOfSubClasses == group.NumOfSubClasses) continue;

				group.HasNoOfSubClasses(newNumberOfSubClasses);
				await _classGroupRepository.UpdateAsync(group, saveChanges: false);

				var sizes = group.Size.GetSubClassSizes(newNumberOfSubClasses);
				for (int i =  0; i < newNumberOfSubClasses; i++)
				{
					var subName = newNumberOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i + 1)}" : group.Name;
					await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i], subName), saveChanges: false);
				}
			}

			var limitSettings = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.GeneralClassSizeLimit);
			if (limitSettings is null)
			{
                await _adminSettingsRepository.AddAsync(AdminSettings
					.Create(AdminConfigurationKeys.GeneralClassSizeLimit, limit.ToString()));
				return true;
            }

			limitSettings.HasValue(limit.ToString());
			await _adminSettingsRepository.UpdateAsync(limitSettings);
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError("Error while adjusting class sizes. Message: {Message}", e.Message);
			return e;
		}
	}

    public async Task AddSubClassGroups()
    {
        var groups = await _classGroupRepository.GetAllAsync()
            ?? throw new DataNotSyncedWithUmatException();

        var subGroups = await _subClassGroupRepository.GetAllAsync();
        await _subClassGroupRepository.DeleteAllAsync(subGroups, saveChanges: false);

        foreach (var group in groups)
        {
            var sizes = group.Size.GetSubClassSizes(group.NumOfSubClasses);
            for (int i = 1; i <= group.NumOfSubClasses; i++)
            {
                var subName = group.NumOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i)}" : group.Name;
                await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i - 1],
                    subName), saveChanges: false);
            }
        }

        await _subClassGroupRepository.SaveChanges();
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
		var @class = await _classGroupRepository.FindByIdAsync(classId, useCache: true);
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

    public async Task<OneOf<bool, Exception>> SetClassSize(int classSize, int classGroupId)
    {
        var group = await _classGroupRepository.FindByIdAsync(classGroupId);
        if (group == null) return new InvalidIdException();

		var generalClassLimit = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.GeneralClassSizeLimit);
		var numOfSubClasses = (classSize / Convert.ToInt32(generalClassLimit?.Value)) + 1;
        group.HasSize(classSize)
			.HasNoOfSubClasses(numOfSubClasses);

		var subClasses = await _subClassGroupRepository.GetAllAsync(x => x.GroupId == group.Id);
		await _subClassGroupRepository.DeleteAllAsync(subClasses, saveChanges: false);

		var sizes = group.Size.GetSubClassSizes(group.NumOfSubClasses);
		for (int i = 1; i <= group.NumOfSubClasses; i++)
		{
			var subName = group.NumOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i)}" : group.Name;
			await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i - 1],
				subName), saveChanges: false);
		}
		await _classGroupRepository.UpdateAsync(group);
        return true;
    }

    public async Task<List<SubClassGroupDto>> GetSubClassGroups(int classGroupId)
    {
        var subs = await _subClassGroupRepository.GetAllAsync(x => x.GroupId == classGroupId);
		var output = subs.Adapt<List<SubClassGroupDto>>();
		output ??= new List<SubClassGroupDto>(0);
		return output;
    }

    public async Task<OneOf<bool, Exception>> UpdateSubClasses(List<SubClassGroupCommand> commands)
    {
        var group = await _classGroupRepository.GetAsync(x => x.Id == commands[0].GroupId);
		if (group is null) return new InvalidIdException();

        var combinedCommandSize = commands.Sum(x => x.Size);
		if (combinedCommandSize != group.Size) return new InvalidSubClassSizesException();
		
		foreach (var command in commands)
		{
			var sub = group.SubClassGroups.FirstOrDefault(x => x.Id == command.Id);
			if (sub is null) continue;
			sub.HasSize(command.Size);
		}

		await _classGroupRepository.UpdateAsync(group);
		return true;
    }
}

public record SubClassGroupCommand(int? Id, int GroupId, int Size, string Name);
public record SubClassGroupDto(int Id, int GroupId, int? Size, string Name, List<LectureDto> Lectures);
public record SubClassGroupPageDto(int Id, int GroupId, int? Size, string Name);
public record ClassGroupDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name, List<SubClassGroupDto> SubClassGroups);
public record ClassGroupPageDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name);
