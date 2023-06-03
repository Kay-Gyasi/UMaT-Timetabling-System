using Microsoft.EntityFrameworkCore.Storage;

namespace UMaTLMS.Core.Repositories.Base;

public interface IRepository<T, TKey> where T : Entity
{
    Task AddAsync(T entity, bool saveChanges = true);
    Task SoftDeleteAsync(T entity, bool saveChanges = true);
    Task DeleteAsync(T entity, bool saveChanges = true);
    Task UpdateAsync(T entity, bool saveChanges = true);
    Task<T?> FindByIdAsync(int id);
    Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command);
    Task<IDbContextTransaction> BeginTransaction();
    Task<bool> SaveChanges();
}