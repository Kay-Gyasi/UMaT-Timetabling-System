namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<IncomingCourse, int>, ICourseRepository
{
    private readonly CacheService _cache;

    public CourseRepository(AppDbContext context, CacheService cache, ILogger<CourseRepository> logger) 
        : base(context, cache, logger)
    {
        _cache = cache;
    }

    public async Task<List<Lookup>> GetLookup()
    {
        return await GetBaseQuery().Select(x => new Lookup(x.Id, x.Name ?? string.Empty)).ToListAsync();
    }

    public override async Task<PaginatedList<IncomingCourse>> GetPageAsync(PaginatedCommand command, 
        IQueryable<IncomingCourse>? source = null, bool cacheEntities = true)
    {
        IEnumerable<IncomingCourse>? distinctData;
        if (_cache.HasKey(nameof(IncomingCourse)))
        {
            var courses = _cache.Get<List<IncomingCourse>>(nameof(IncomingCourse));
            if (!string.IsNullOrWhiteSpace(command.Search) && courses is not null)
            {
                courses = courses.Where(x => x.Name != null && x.Name.Contains(command.Search, StringComparison.OrdinalIgnoreCase)
                            || x.Code != null && x.Code.Contains(command.Search, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }

            if (courses is not null) 
            {
                distinctData = courses.DistinctBy(x => x.Name);
                return await GeneratePageData(command, distinctData);
            };
        }
        
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = GetBaseQuery().Where(x => x.Name != null && x.Name.ToLower().Contains(command.Search.ToLower())
                || x.Code != null && x.Code.ToLower().Contains(command.Search.ToLower()));
        }

        var data = source is not null ? source.AsNoTracking().AsSingleQuery() : GetBaseQuery().AsNoTracking().AsSingleQuery();
        if (cacheEntities) _cache.StoreEntities(typeof(IncomingCourse).Name, GetBaseQuery().ToList());
        distinctData = data.AsEnumerable().DistinctBy(x => x.Name);
        return await GeneratePageData(command, distinctData);
    }

    private async Task<PaginatedList<IncomingCourse>> GeneratePageData(PaginatedCommand command, IEnumerable<IncomingCourse> distinctData)
    {
        return await Task.Run(() => 
        {
            var count = distinctData.Count();
            var items = distinctData
                            .OrderBy(x => x.Name)
                            .Skip((command.PageNumber - 1) * command.PageSize)
                            .Take(command.PageSize)
                            .ToList();
            return new PaginatedList<IncomingCourse>(items, count, command.PageNumber, command.PageSize);
        });
    }
}