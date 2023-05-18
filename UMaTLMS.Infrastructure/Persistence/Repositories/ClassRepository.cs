namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class ClassRepository : Repository<Class, int>, IClassRepository
{
    public ClassRepository(AppDbContext context, ILogger<Repository<Class, int>> logger) : base(context, logger)
    {
    }

    protected override IQueryable<Class> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Department);
    }
}