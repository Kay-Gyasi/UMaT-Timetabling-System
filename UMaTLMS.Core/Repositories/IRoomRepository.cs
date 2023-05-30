using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IRoomRepository : IRepository<ClassRoom, int>
{
    Task<bool> Exists(string name);
    Task<IEnumerable<ClassRoom>> GetAllAsync();
    Task<bool> IsInitialized();
}
