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
        var @class = await _classGroupRepository.FindByIdAsync(classGroupId, useCache: true);
        if (@class == null) return new InvalidIdException();

		@class.HasSize(classSize);

		// TODO:: Save class limit and use to determine NumOfSubClasses

        //var sizes = @class.Size.GetSubClassSizes(@class.NumOfSubClasses);
        //for (int i = 1; i <= @class.NumOfSubClasses; i++)
        //{
        //    var subName = group.NumOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i)}" : group.Name;
        //    await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i - 1],
        //        subName), saveChanges: false);
        //}
        await _classGroupRepository.UpdateAsync(@class);
        return true;
    }

    public async Task<List<SubClassGroupDto>> GetSubClassGroups(int classGroupId)
    {
        var subs = await _subClassGroupRepository.GetAllAsync(x => x.GroupId == classGroupId);
		var output = subs.Adapt<List<SubClassGroupDto>>();
		output ??= new List<SubClassGroupDto>(0);
		return output;
    }
}

public record SubClassGroupCommand(int? Id, int GroupId, int? Size, string Name);
public record SubClassGroupDto(int Id, int GroupId, int? Size, string Name, List<LectureDto> Lectures);
public record SubClassGroupPageDto(int Id, int GroupId, int? Size, string Name);
public record ClassGroupDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name, List<SubClassGroupDto> SubClassGroups);
public record ClassGroupPageDto(int Id, int UmatId, int? Size, int NumOfSubClasses, string Name);
