namespace UMaTLMS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public AppDbContext() { }

    public DbSet<ClassRoom> Rooms { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<LectureSchedule> Schedules { get; set; }
    public DbSet<OnlineLectureSchedule> OnlineSchedules { get; set; }
    public DbSet<IncomingCourse> IncomingCourses { get; set; }
    public DbSet<ClassGroup> ClassGroups { get; set; }
    public DbSet<SubClassGroup> SubClassGroups { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Lecture> Lectures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.Development);
        }
    }
}