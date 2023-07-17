using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class ClassGroupRepository : Repository<ClassGroup, int>, IClassGroupRepository
{
    private readonly CacheService _cache;

    public ClassGroupRepository(AppDbContext context, CacheService cache, ILogger<ClassGroupRepository> logger) 
        : base(context, cache, logger)
    {
        _cache = cache;
    }

    public async Task<List<ClassGroup>> GetAll()
    {
        return await GetBaseQuery().ToListAsync();
    }

    public async Task<bool> Exists(string name)
    {
        return await GetBaseQuery().AnyAsync(x => x.Name == name);
    }

    protected override IQueryable<ClassGroup> GetBaseQuery()
    {
	    return base.GetBaseQuery()
                .Include(x => x.SubClassGroups);
    }

    public override Task<PaginatedList<ClassGroup>> GetPageAsync(PaginatedCommand command, IQueryable<ClassGroup>? source = null,
            bool cacheEntities = false)
    {
        if (string.IsNullOrEmpty(command.Search))
        {
            return base.GetPageAsync(command);
        }

        source = base.GetBaseQuery().Where(x => x.Name.Contains(command.Search ?? ""));
        return base.GetPageAsync(command, source);
    }
}