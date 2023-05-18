namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class DepartmentRepository : Repository<Department, int>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context, ILogger<Repository<Department, int>> logger) : base(context, logger)
    {
    }

    public async Task<bool> Exists(string code)
    {
        return await GetBaseQuery().AnyAsync(x => x.Code == code);
    }
}