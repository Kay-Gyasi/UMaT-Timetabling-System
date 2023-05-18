namespace LMSData.Courses;

public class CourseConfiguration : DatabaseConfiguration<Course, int>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);
        builder.ToTable(DomainEntities.Course);
    }
}