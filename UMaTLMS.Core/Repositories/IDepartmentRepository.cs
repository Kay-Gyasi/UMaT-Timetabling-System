using UMaTLMS.Infrastructure.Persistence.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IDepartmentRepository : IRepository<Department, int>
{
    Task<bool> Exists(string code);
}
