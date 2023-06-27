using System.Drawing.Printing;
using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Infrastructure.Persistence.Repositories.Base;

public class Repository<T, TKey> : IRepository<T, TKey>
    where T : Entity
{
    protected readonly AppDbContext Context;
    private readonly ILogger<Repository<T, TKey>> _logger;
    private DbSet<T>? _dbSet;
    protected Repository(AppDbContext context, ILogger<Repository<T, TKey>> logger)
    {
        Context = context;
        _logger = logger;
    }

    protected virtual DbSet<T> Entities
        => _dbSet ??= Context.Set<T>();

    protected virtual IQueryable<T> GetBaseQuery()
        => Entities;

    public async Task AddAsync(T entity, bool saveChanges = true)
    {
        try
        {
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
            await Task.Run(() => Entities.Update(entity));
            if (saveChanges) await Context.SaveChangesAsync();
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
            await Task.Run(() => entity.Audit?.Delete());
            if (saveChanges) await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        try
        {
            if (id == 0) return null;
            var keyProperty = Context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];
            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>
                (e, keyProperty!.Name) == id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }

    public virtual async Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command, IQueryable<T>? source = null)
    {
        return await Task.Run(() =>
        {
            var data = source is not null ? source.AsSingleQuery() : GetBaseQuery().AsSingleQuery();
            var count = data.Count();
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