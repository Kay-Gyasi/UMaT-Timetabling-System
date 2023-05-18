namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class StudentRepository : Repository<Student, int>, IStudentRepository
{
    public StudentRepository(AppDbContext context, ILogger<Repository<Student, int>> logger)
        : base(context, logger)
    {
    }
}