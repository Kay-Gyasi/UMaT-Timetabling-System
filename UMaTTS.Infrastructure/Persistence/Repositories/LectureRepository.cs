using Microsoft.Extensions.Caching.Memory;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    // Use IMemoryCache to cache pages
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
            bool cacheEntities = false)
        {
            if (_cache.HasKey(nameof(Lecture)))
            {
                var lectures = _cache.Get<List<Lecture>>(nameof(Lecture));
                if (lectures is not null) return base.GetPageAsync(command, lectures);
            }

            source ??= GetBaseQuery().OrderBy(x => x.Course!.Name);
            if (!string.IsNullOrWhiteSpace(command.Search))
            {
                source = source.Where(x => x.Lecturer!.Name!.Contains(command.Search) 
                            || x.Course!.Name!.Contains(command.Search) 
                            || x.SubClassGroups.Any(g => g.Name.Contains(command.Search)));
            }

            return base.GetPageAsync(command, source, cacheEntities: true);
        }
    }
}