using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IClassGroupRepository : IRepository<ClassGroup, int>
{
    Task<List<ClassGroup>> GetAll();
    Task<bool> Exists(string name);
}