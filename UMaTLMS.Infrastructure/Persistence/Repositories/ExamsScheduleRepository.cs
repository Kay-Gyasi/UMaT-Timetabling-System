namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class ExamsScheduleRepository : Repository<ExamsSchedule, int>, IExamsScheduleRepository
{
    public ExamsScheduleRepository(AppDbContext context, ILogger<ExamsScheduleRepository> logger) 
        : base(context, logger)
    {
    }

    public async Task<List<ExamsSchedule>> GetAll()
    {
        return await GetBaseQuery().ToListAsync();
    }
}