namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : DatabaseConfiguration<IncomingCourse, int>
{
    public override void Configure(EntityTypeBuilder<IncomingCourse> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Code);
        builder.OwnsOne(x => x.AcademicPeriod);
    }
}