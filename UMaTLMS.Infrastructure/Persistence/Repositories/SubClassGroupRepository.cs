namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class SubClassGroupRepository : Repository<SubClassGroup, int>, ISubClassGroupRepository
    {
        public SubClassGroupRepository(AppDbContext context, ILogger<SubClassGroupRepository> logger) : base(context, logger)
        {
        }

        public async Task<List<SubClassGroup>> GetAll()
        {
            return await GetBaseQuery().ToListAsync();
        }
    }
}