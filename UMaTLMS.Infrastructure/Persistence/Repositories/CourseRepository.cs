namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<IncomingCourse, int>, ICourseRepository
{
    public CourseRepository(AppDbContext context, ILogger<CourseRepository> logger) : base(context, logger)
    {
    }

    public async Task<List<IncomingCourse>> GetAll()
    {
        return await GetBaseQuery().ToListAsync();
    }
}