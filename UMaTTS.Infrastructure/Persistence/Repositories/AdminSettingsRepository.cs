namespace UMaTLMS.Infrastructure.Persistence.Repositories;
public class AdminSettingsRepository : Repository<AdminSettings, int>, IAdminSettingsRepository
{
    public AdminSettingsRepository(AppDbContext context, CacheService cache, ILogger<Repository<AdminSettings, int>> logger) : base(context, cache, logger)
    {
    }
}
