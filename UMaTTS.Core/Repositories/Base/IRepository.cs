﻿using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace UMaTLMS.Core.Repositories.Base;

public interface IRepository<T, TKey> where T : Entity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool useCache = false);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task AddAsync(T entity, bool saveChanges = true);
    Task SoftDeleteAsync(T entity, bool saveChanges = true);
    Task DeleteAsync(T entity, bool saveChanges = true);
    Task DeleteAllAsync(List<T> entities, bool saveChanges = true);
    Task UpdateAsync(T entity, bool saveChanges = true);
    Task<T?> FindByIdAsync(int id, bool useCache = false);
    Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command, IQueryable<T>? source = null, bool cacheEntities = true);
    Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command, List<T> data);
    Task<IDbContextTransaction> BeginTransaction();
    Task<bool> SaveChanges();
}