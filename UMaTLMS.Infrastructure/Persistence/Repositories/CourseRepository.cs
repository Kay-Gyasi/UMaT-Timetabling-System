namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<Course, int>, ICourseRepository
{
    public CourseRepository(AppDbContext context, ILogger<Repository<Course, int>> logger) : base(context, logger)
    {
    }

    protected override IQueryable<Course> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Department);
    }
}