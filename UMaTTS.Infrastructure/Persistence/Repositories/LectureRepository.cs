namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LectureRepository : Repository<Lecture, int>, ILectureRepository
    {
        public LectureRepository(AppDbContext context, ILogger<LectureRepository> logger) : base(context, logger)
        {
        }

        protected override IQueryable<Lecture> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.SubClassGroups)
                .Include(x => x.Lecturer)
                .Include(x => x.Course);
        }

        public override Task<PaginatedList<Lecture>> GetPageAsync(PaginatedCommand command, IQueryable<Lecture>? source = null)
        {
            source = GetBaseQuery().OrderByDescending(x => x.Course!.Name);
            if (!string.IsNullOrWhiteSpace(command.Search))
            {
                source = source.Where(x => x.Lecturer!.Name!.Contains(command.Search) 
                            || x.Course!.Name!.Contains(command.Search) 
                            || x.SubClassGroups.Any(g => g.Name.Contains(command.Search)));
            }
            
            return base.GetPageAsync(command, source);
        }
    }
}