namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class RoomRepository : Repository<ClassRoom, int>, IRoomRepository
{
    private readonly CacheService _cache;

    public RoomRepository(AppDbContext context, CacheService cache, ILogger<RoomRepository> logger)
        : base(context, cache, logger)
    {
        _cache = cache;
    }


    public async Task<bool> Exists(string name)
    {
        return await GetBaseQuery().AnyAsync(x => x.Name == name);
    }

    public async Task<bool> IsInitialized()
    {
        return await GetBaseQuery().AnyAsync();
    }

    public override Task<PaginatedList<ClassRoom>> GetPageAsync(PaginatedCommand command, IQueryable<ClassRoom>? source = null,
            bool cacheEntities = true)
    {
        if (_cache.HasKey(nameof(ClassRoom)))
        {
            var rooms = _cache.Get<List<ClassRoom>>(nameof(ClassRoom));
            if (!string.IsNullOrWhiteSpace(command.Search) && rooms is not null)
            {
                rooms = rooms.Where(x => x.Name.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (rooms is not null) return base.GetPageAsync(command, rooms);
        }

        source = GetBaseQuery().OrderBy(x => x.Name);
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = source.Where(x => x.Name.ToLower().Contains(command.Search.ToLower()));
        }
        return base.GetPageAsync(command, source);
    }

    public async Task<List<Lookup>> GetLookup()
    {
        return await GetBaseQuery()
                .OrderBy(x => x.Name)
                .Select(x => new Lookup(x.Id, x.Name))
                .ToListAsync();
    }
}