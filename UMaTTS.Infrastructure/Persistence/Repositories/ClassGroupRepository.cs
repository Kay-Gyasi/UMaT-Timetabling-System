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
        if (_cache.HasKey(nameof(ClassGroup))) 
            return _cache.Get<List<ClassGroup>>(nameof(ClassGroup)) ?? new List<ClassGroup>();
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
            bool cacheEntities = true)
    {
        if (_cache.HasKey(nameof(ClassGroup)))
        {
            var classes = _cache.Get<List<ClassGroup>>(nameof(ClassGroup));   
            if (!string.IsNullOrEmpty(command.Search) && classes is not null)
            {
                classes = classes.Where(x => x.Name != null 
                    && x.Name.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }         
            if (classes is not null) return base.GetPageAsync(command, classes);
        }
        
        if (string.IsNullOrEmpty(command.Search)) return base.GetPageAsync(command);

        source = GetBaseQuery().Where(x => x.Name != null 
                    && x.Name.ToLower().Contains(command.Search.ToLower()));
        return base.GetPageAsync(command, source);
    }
}