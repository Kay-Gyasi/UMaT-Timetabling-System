using System.Linq.Expressions;
using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Infrastructure.Persistence.Repositories.Base;

public class Repository<T, TKey> : IRepository<T, TKey>
    where T : Entity
{
    protected readonly AppDbContext Context;
    private readonly CacheService _cache;
    private readonly ILogger<Repository<T, TKey>> _logger;
    private DbSet<T>? _dbSet;
    protected Repository(AppDbContext context, CacheService cache, ILogger<Repository<T, TKey>> logger)
    {
        Context = context;
        _cache = cache;
        _logger = logger;
    }

    protected virtual DbSet<T> Entities
        => _dbSet ??= Context.Set<T>();

    protected virtual IQueryable<T> GetBaseQuery()
        => Entities;

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        try
        {
            predicate ??= x => true;
            var results = await GetBaseQuery().Where(predicate).ToListAsync();
            return results;
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }
    
    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool useCache = false)
    {
        try
        {
            if (predicate == null) return null;
            if (useCache && _cache.HasKey(typeof(T).Name))
            {
                var entities = await _cache.Get<List<T>>(typeof(T).Name)!
                    .AsQueryable()
                    .FirstOrDefaultAsync(predicate);
            }

            return await GetBaseQuery().FirstOrDefaultAsync(predicate);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task AddAsync(T entity, bool saveChanges = true)
    {
        try
        {
            _cache.Remove<T>();
            await Entities.AddAsync(entity);
            if (saveChanges)
                await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task UpdateAsync(T entity, bool saveChanges = true)
    {
        try
        {
            _cache.Remove<T>();
            await Task.Run(() => Entities.Update(entity));
            if (saveChanges) 
                await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task DeleteAsync(T entity, bool saveChanges = true)
    {
        try
        {
            _cache.Remove<T>();
            await Task.Run(() => Entities.Remove(entity));
            if (saveChanges) await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }
    
    public async Task DeleteAllAsync(List<T> entities, bool saveChanges = true)
    {
        try
        {
            _cache.Remove<T>();
            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    Entities.Remove(entity);
                }
            });

            if (saveChanges) await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task SoftDeleteAsync(T entity, bool saveChanges = true)
    {
        try
        {
            _cache.Remove<T>();
            await Task.Run(() => entity.Audit?.Delete());
            if (saveChanges) await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task<T?> FindByIdAsync(int id, bool useCache = false)
    {
        try
        {
            if (id == 0) return null;
            var keyProperty = Context.Model
                                .FindEntityType(typeof(T))?
                                .FindPrimaryKey()?
                                .Properties[0];

            if (useCache && _cache.HasKey(typeof(T).Name))
            {
                return _cache.Get<List<T>>(typeof(T).Name)!
                    .FirstOrDefault(x => x.Id == id);
            }

            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty!.Name) == id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public virtual async Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command, IQueryable<T>? source = null,
        bool cacheEntities = true)
    {
        return await Task.Run(() =>
        {
            var data = source is not null ? source.AsNoTracking().AsSingleQuery() : GetBaseQuery().AsNoTracking().AsSingleQuery();
            var count = data.Count();
            var items = data
                            .Skip((command.PageNumber - 1) * command.PageSize)
                            .Take(command.PageSize)
                            .ToList();

            if (cacheEntities) _cache.StoreEntities(typeof(T).Name, GetBaseQuery().AsNoTracking().ToList());
            return new PaginatedList<T>(items, count, command.PageNumber, command.PageSize);
        });
    }
    
    public virtual async Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command, List<T> data)
    {
        return await Task.Run(() =>
        {
            var count = data.Count;
            var items = data.Skip((command.PageNumber - 1) * command.PageSize)
                            .Take(command.PageSize)
                            .ToList();
            return new PaginatedList<T>(items, count, command.PageNumber, command.PageSize);
        });
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await Context.Database.BeginTransactionAsync();
    }

    public async Task<bool> SaveChanges()
    {
        return await Context.SaveChangesAsync() > 0;
    }
}