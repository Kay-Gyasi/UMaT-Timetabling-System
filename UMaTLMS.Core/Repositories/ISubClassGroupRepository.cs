using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories
{
    public interface ISubClassGroupRepository : IRepository<SubClassGroup, int>
    {
        Task<List<SubClassGroup>> GetAll();
        Task<bool> IsValid(int groupId, int capacity, string name);
    }
}