namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class OnlineLectureScheduleRepository : Repository<OnlineLectureSchedule, int>, IOnlineLectureScheduleRepository
{
    public OnlineLectureScheduleRepository(AppDbContext context, CacheService cache, ILogger<OnlineLectureScheduleRepository> logger) 
        : base(context, cache, logger)
    {
    }

    protected override IQueryable<OnlineLectureSchedule> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Lectures);
    }
}
