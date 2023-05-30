namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LectureRepository : Repository<Lecture, int>, ILectureRepository
    {
        public LectureRepository(AppDbContext context, ILogger<LectureRepository> logger) : base(context, logger)
        {
        }

        public async Task<List<Lecture>> GetAll()
        {
            return await GetBaseQuery().ToListAsync();
        }

        protected override IQueryable<Lecture> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.SubClassGroups);
        }
    }
}