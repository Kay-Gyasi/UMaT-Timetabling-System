
using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LecturerRepository : Repository<Lecturer, int>, ILecturerRepository
    {
        public LecturerRepository(AppDbContext context, CacheService cache, ILogger<LecturerRepository> logger) 
            : base(context, cache, logger)
        {
        }

        public async Task<bool> Exists(int umatId)
        {
            return await GetBaseQuery().AnyAsync(x => x.UmatId == umatId);
        }
    }
}