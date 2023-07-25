using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IConstraintRepository : IRepository<Constraint, int>
{
    Task<bool> IsInitialized();
}
