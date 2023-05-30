namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : DatabaseConfiguration<IncomingCourse, int>
{
    public override void Configure(EntityTypeBuilder<IncomingCourse> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(x => x.AcademicPeriod);
    }
}