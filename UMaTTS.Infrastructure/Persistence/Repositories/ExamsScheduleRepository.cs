using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class ExamsScheduleRepository : Repository<ExamsSchedule, int>, IExamsScheduleRepository
{
    public ExamsScheduleRepository(AppDbContext context, CacheService cache, ILogger<ExamsScheduleRepository> logger) 
        : base(context, cache, logger)
    {
    }

    public async Task<List<ExamsSchedule>> GetAll()
    {
        return await GetBaseQuery().ToListAsync();
    }
}