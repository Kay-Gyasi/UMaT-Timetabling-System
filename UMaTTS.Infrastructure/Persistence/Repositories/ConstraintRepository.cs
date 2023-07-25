namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class ConstraintRepository : Repository<Constraint, int>, IConstraintRepository
{
    public ConstraintRepository(AppDbContext context, CacheService cache, ILogger<ConstraintRepository> logger) 
        : base(context, cache, logger)
    {
    }

    public async Task<bool> IsInitialized()
    {
        return await GetBaseQuery().AnyAsync();
    }
}