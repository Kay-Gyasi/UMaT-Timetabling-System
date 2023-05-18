using UMaTLMS.Infrastructure.Persistence.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IRoomRepository : IRepository<Room, int>
{
    Task<bool> Exists(string name);
}
