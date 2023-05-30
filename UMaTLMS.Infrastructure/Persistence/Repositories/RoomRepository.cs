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
    
    public async Task<IEnumerable<ClassRoom>> GetAllAsync()
    {
        return await GetBaseQuery().ToListAsync();
    }

    public async Task<bool> IsInitialized()
    {
        return await GetBaseQuery().AnyAsync();
    }
}