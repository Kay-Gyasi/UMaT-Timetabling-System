namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class PreferenceRepository : Repository<Preference, int>, IPreferenceRepository
{
    private readonly CacheService _cache;

    public PreferenceRepository(AppDbContext context, CacheService cache, ILogger<PreferenceRepository> logger) 
        : base(context, cache, logger)
    {
        _cache = cache;
    }

    public async Task<PaginatedList<Preference>> GetLecturerPreferences(PaginatedCommand command)
    {
        if (_cache.HasKey(nameof(Preference)))
        {
            var preferences = _cache.Get<List<Preference>>(nameof(Preference));
            if(preferences is not null) {
                preferences = preferences.Where(x => x.LecturerId != null).ToList();
                if (!string.IsNullOrWhiteSpace(command.Search))
                {
                    preferences = preferences.Where(x => x.Lecturer!.TitledName!.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return await base.GetPageAsync(command, preferences);
            }
        }
            
        var source = GetBaseQuery().Where(x => x.LecturerId != null).AsNoTracking();
        _cache.StoreEntities(nameof(Preference), source.AsNoTracking().ToList());
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = source.Where(x => x.Lecturer!.TitledName!.ToLower().Contains(command.Search.ToLower()));
        }
        return await base.GetPageAsync(command, source);
    }
    
    public async Task<PaginatedList<Preference>> GetCoursePreferences(PaginatedCommand command)
    {
        if (_cache.HasKey(nameof(Preference)))
        {
            var preferences = _cache.Get<List<Preference>>(nameof(Preference));
            if(preferences is not null) {
                preferences = preferences.Where(x => x.CourseId != null).ToList();
                if (!string.IsNullOrWhiteSpace(command.Search))
                {
                    preferences = preferences.Where(x => x.Course!.Name!.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return await base.GetPageAsync(command, preferences);
            }
        }
            
        var source = GetBaseQuery().Where(x => x.CourseId != null).AsNoTracking();
        _cache.StoreEntities(nameof(Preference), source.AsNoTracking().ToList());
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = source.Where(x => x.Course!.Name!.ToLower().Contains(command.Search.ToLower()));
        }
        return await base.GetPageAsync(command, source);
    }

    public override Task<PaginatedList<Preference>> GetPageAsync(PaginatedCommand command, IQueryable<Preference>? source = null, bool cacheEntities = true)
    {
        if (_cache.HasKey(nameof(Preference)))
        {
            var preferences = _cache.Get<List<Preference>>(nameof(Preference));
            if (!string.IsNullOrWhiteSpace(command.Search) && preferences is not null)
            {
                preferences = preferences.Where(x => x.Lecturer!.TitledName!.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (preferences is not null) return base.GetPageAsync(command, preferences);
        }

        source = GetBaseQuery();
        _cache.StoreEntities(nameof(Preference), source.AsNoTracking().ToList());
        if (!string.IsNullOrWhiteSpace(command.Search))
        {
            source = source.Where(x => x.Lecturer!.TitledName!.ToLower().Contains(command.Search.ToLower()));
        }
        return base.GetPageAsync(command, source);
    }

    protected override IQueryable<Preference> GetBaseQuery()
    {
        return base.GetBaseQuery()
                    .Include(x => x.Lecturer)
                    .Include(x => x.Course);
    }
}
