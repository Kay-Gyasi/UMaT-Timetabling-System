namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class LecturerRepository : Repository<Lecturer, int>, ILecturerRepository
    {
        private readonly CacheService _cache;

        public LecturerRepository(AppDbContext context, CacheService cache, ILogger<LecturerRepository> logger) 
            : base(context, cache, logger)
        {
            _cache = cache;
        }

        public async Task<bool> Exists(int umatId)
        {
            return await GetBaseQuery().AnyAsync(x => x.UmatId == umatId);
        }

        public async Task<List<Lookup>> GetLookup()
        {
            return await GetBaseQuery().Select(x => 
                    new Lookup(x.Id, x.TitledName ?? string.Empty)).ToListAsync();
        }

        public override Task<PaginatedList<Lecturer>> GetPageAsync(PaginatedCommand command, IQueryable<Lecturer>? source = null,
            bool cacheEntities = true)
        {
            if (_cache.HasKey(nameof(Lecturer)))
            {
                var lecturers = _cache.Get<List<Lecturer>>(nameof(Lecturer));
                if (!string.IsNullOrWhiteSpace(command.Search) && lecturers is not null)
                {
                    lecturers = lecturers.Where(x => x.Name!.Contains(command.Search, StringComparison.OrdinalIgnoreCase)
                                                || x.TitledName!.Contains(command.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (lecturers is not null) return base.GetPageAsync(command, lecturers);
            }

            source = GetBaseQuery().OrderBy(x => x.Name);
            _cache.StoreEntities(typeof(Lecturer).Name, source.AsNoTracking().ToList());
            if (!string.IsNullOrWhiteSpace(command.Search))
            {
                source = source.Where(x => x.Name!.ToLower().Contains(command.Search.ToLower())
                                            || x.TitledName!.ToLower().Contains(command.Search.ToLower()));
            }
            return base.GetPageAsync(command, source);
        }
    }
}