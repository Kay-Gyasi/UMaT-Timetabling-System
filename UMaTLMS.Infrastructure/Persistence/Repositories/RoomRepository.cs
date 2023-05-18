namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class RoomRepository : Repository<Room, int>, IRoomRepository
{
    public RoomRepository(AppDbContext context, ILogger<Repository<Room, int>> logger)
        : base(context, logger)
    {
    }


    public async Task<bool> Exists(string name)
    {
        return await GetBaseQuery().AnyAsync(x => x.Name == name);
    }
}