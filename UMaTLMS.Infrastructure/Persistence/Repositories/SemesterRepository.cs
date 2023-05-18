namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class SemesterRepository : Repository<Semester, int>, ISemesterRepository
{
    public SemesterRepository(AppDbContext context, ILogger<Repository<Semester, int>> logger)
        : base(context, logger)
    {
    }
}