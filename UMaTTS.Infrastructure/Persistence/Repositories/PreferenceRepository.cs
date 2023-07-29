namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class PreferenceRepository : Repository<Preference, int>, IPreferenceRepository
{
    public PreferenceRepository(AppDbContext context, CacheService cache, ILogger<PreferenceRepository> logger) 
        : base(context, cache, logger)
    {
    }

    public async Task<List<Preference>> GetLecturerPreferences()
    {
        return await GetBaseQuery().AsNoTracking().ToListAsync();
    }

    protected override IQueryable<Preference> GetBaseQuery()
    {
        return base.GetBaseQuery()
                    .Include(x => x.Lecturer)
                    .Include(x => x.Course);
    }
}
