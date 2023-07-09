namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class RoomRepository : Repository<ClassRoom, int>, IRoomRepository
{
    public RoomRepository(AppDbContext context, ILogger<RoomRepository> logger)
        : base(context, logger)
    {
    }


    public async Task<bool> Exists(string name)
    {
        return await GetBaseQuery().AnyAsync(x => x.Name == name);
    }

    public async Task<bool> IsInitialized()
    {
        return await GetBaseQuery().AnyAsync();
    }

    public override Task<PaginatedList<ClassRoom>> GetPageAsync(PaginatedCommand command, IQueryable<ClassRoom>? source = null)
    {
        source = GetBaseQuery().OrderBy(x => x.Name);
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = source.Where(x => x.Name.Contains(command.Search));
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