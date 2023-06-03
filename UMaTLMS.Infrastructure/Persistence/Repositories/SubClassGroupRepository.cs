namespace UMaTLMS.Infrastructure.Persistence.Repositories
{
    public class SubClassGroupRepository : Repository<SubClassGroup, int>, ISubClassGroupRepository
    {
		private readonly AppDbContext _context;

		public SubClassGroupRepository(AppDbContext context, ILogger<SubClassGroupRepository> logger) : base(context, logger)
        {
			_context = context;
		}

        public async Task<List<SubClassGroup>> GetAll()
        {
            return await GetBaseQuery().ToListAsync();
        }

		public async Task<bool> IsValid(int groupId, int capacity, string name)
		{
			var isValidName = await GetBaseQuery().AnyAsync(x => x.Name != name);
			if (!isValidName) return false;

            var group = _context.ClassGroups.FirstOrDefault(x => x.Id == groupId);
			if (group == null) return false;

			var occupiedSpace = group.SubClassGroups.Select(x => x.Size).Sum();
			return (occupiedSpace + capacity) <= group.Size;
		}
	}
}