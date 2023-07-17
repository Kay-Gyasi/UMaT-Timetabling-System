using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<IncomingCourse, int>, ICourseRepository
{
    public CourseRepository(AppDbContext context, CacheService cache, ILogger<CourseRepository> logger) 
        : base(context, cache, logger)
    {
    }

    public override async Task<PaginatedList<IncomingCourse>> GetPageAsync(PaginatedCommand command, 
        IQueryable<IncomingCourse>? source = null, bool cacheEntities = false)
    {
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = GetBaseQuery().Where(x => x.Name != null && x.Name.Contains(command.Search)
                || x.Code != null && x.Code.Contains(command.Search));
        }

        return await Task.Run(() =>
        {
            var data = source is not null ? source.AsSingleQuery() : GetBaseQuery().AsSingleQuery();
            var distinctData = data.AsEnumerable().DistinctBy(x => x.Name);
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