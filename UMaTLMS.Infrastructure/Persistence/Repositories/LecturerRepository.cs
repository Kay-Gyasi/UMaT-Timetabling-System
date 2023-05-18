namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class LecturerRepository : Repository<Lecturer, int>, ILecturerRepository
{
    public LecturerRepository(AppDbContext context, ILogger<Repository<Lecturer, int>> logger)
        : base(context, logger)
    {
    }

    protected override IQueryable<Lecturer> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.User);
    }
}