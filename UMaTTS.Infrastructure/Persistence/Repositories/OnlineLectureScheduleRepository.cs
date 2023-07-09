namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class OnlineLectureScheduleRepository : Repository<OnlineLectureSchedule, int>, IOnlineLectureScheduleRepository
{
    public OnlineLectureScheduleRepository(AppDbContext context, ILogger<OnlineLectureScheduleRepository> logger) : base(context, logger)
    {
    }

    protected override IQueryable<OnlineLectureSchedule> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Lectures);
    }
}