using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories
{
    public interface ISubClassGroupRepository : IRepository<SubClassGroup, int>
    {
        Task<List<SubClassGroup>> GetAll();
    }
}