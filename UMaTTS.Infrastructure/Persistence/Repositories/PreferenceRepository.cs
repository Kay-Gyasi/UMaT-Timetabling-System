namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class PreferenceRepository : Repository<Preference, int>, IPreferenceRepository
{
    public PreferenceRepository(AppDbContext context, CacheService cache, ILogger<PreferenceRepository> logger) 
        : base(context, cache, logger)
    {
    }
}
