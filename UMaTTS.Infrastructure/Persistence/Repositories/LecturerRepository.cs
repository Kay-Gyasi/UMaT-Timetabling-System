
namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LecturerRepository : Repository<Lecturer, int>, ILecturerRepository
    {
        public LecturerRepository(AppDbContext context, ILogger<LecturerRepository> logger) : base(context, logger)
        {
        }

        public async Task<bool> Exists(int umatId)
        {
            return await GetBaseQuery().AnyAsync(x => x.UmatId == umatId);
        }

        public async Task<List<Lecturer>> GetAll()
        {
            return await GetBaseQuery().ToListAsync();
        }
    }
}