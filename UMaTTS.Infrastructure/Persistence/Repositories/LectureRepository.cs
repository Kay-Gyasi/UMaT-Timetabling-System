using Microsoft.Extensions.Caching.Memory;

namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LectureRepository : Repository<Lecture, int>, ILectureRepository
    {
        private readonly CacheService _cache;

        public LectureRepository(AppDbContext context, CacheService cache, ILogger<LectureRepository> logger) 
            : base(context, cache, logger)
        {
            _cache = cache;
        }

        protected override IQueryable<Lecture> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.SubClassGroups)
                .Include(x => x.Lecturer)
                .Include(x => x.Course);
        }

        public override Task<PaginatedList<Lecture>> GetPageAsync(PaginatedCommand command, IQueryable<Lecture>? source = null,
            bool cacheEntities = true)
        {
            if (_cache.HasKey(nameof(Lecture)))
            {
                var lectures = _cache.Get<List<Lecture>>(nameof(Lecture));
                if (!string.IsNullOrWhiteSpace(command.Search) && lectures is not null)
                {
                    lectures = lectures.Where(x => x.Lecturer!.Name!.Contains(command.Search, StringComparison.OrdinalIgnoreCase) 
                                || x.Course!.Name!.Contains(command.Search, StringComparison.OrdinalIgnoreCase) 
                                || x.SubClassGroups.Any(g => g.Name.Contains(command.Search, StringComparison.OrdinalIgnoreCase)))
                                .ToList();
                }
                if (lectures is not null) return base.GetPageAsync(command, lectures);
            }

            source ??= GetBaseQuery().OrderBy(x => x.Course!.Name);
            if (!string.IsNullOrWhiteSpace(command.Search))
            {
                source = source.Where(x => x.Lecturer!.Name!.ToLower().Contains(command.Search.ToLower()) 
                            || x.Course!.Name!.ToLower().Contains(command.Search.ToLower()) 
                            || x.SubClassGroups.Any(g => g.Name.ToLower().Contains(command.Search.ToLower())));
            }

            return base.GetPageAsync(command, source);
        }
    }
}