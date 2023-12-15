using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class SubClassProcessor
{
    private readonly IClassGroupRepository _classGroupRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;

    public SubClassProcessor(IClassGroupRepository classGroupRepository, ISubClassGroupRepository subClassGroupRepository)
    {
        _classGroupRepository = classGroupRepository;
        _subClassGroupRepository = subClassGroupRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(SubClassGroupCommand command)
    {
        var isValid = await _subClassGroupRepository.IsValid(command.GroupId, command.Size, command.Name);
        if (!isValid) return new InvalidDataException();

        var subClass = SubClassGroup.Create(command.GroupId, command.Size, command.Name);
        await _subClassGroupRepository.AddAsync(subClass);
        return subClass.Id;
    }

    public async Task<SubClassGroupDto?> GetAsync(int subClassId)
    {
        var sub = await _subClassGroupRepository.FindByIdAsync(subClassId, useCache: true);
        if (sub is null) return null;

        return sub.Adapt<SubClassGroupDto>(Mapping.GetTypeAdapterConfig());
    }

    public async Task<PaginatedList<SubClassGroupPageDto>?> GetPageAsync(PaginatedCommand command)
    {
        var paginated = await _subClassGroupRepository.GetPageAsync(command);
        return paginated.Adapt<PaginatedList<SubClassGroupPageDto>?>(Mapping.GetTypeAdapterConfig());
    }

    public async Task<bool> DeleteAllAsync(int classId)
    {
        var @class = await _classGroupRepository.FindByIdAsync(classId);
        if (@class == null) return false;

        foreach (var sub in @class.SubClassGroups)
        {
            await _subClassGroupRepository.DeleteAsync(sub, false).ConfigureAwait(false);
        }

        return await _subClassGroupRepository.SaveChanges();
    }

    public async Task<bool> DeleteAsync(int subClassId)
    {
        var sub = await _subClassGroupRepository.FindByIdAsync(subClassId);
        if (sub == null) return false;

        await _subClassGroupRepository.DeleteAsync(sub);
        return true;
    }
}
