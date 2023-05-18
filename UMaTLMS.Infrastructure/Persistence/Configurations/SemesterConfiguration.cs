using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LMSData.Semesters;

public class SemesterConfiguration : DatabaseConfiguration<Semester, int>
{
    public override void Configure(EntityTypeBuilder<Semester> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Type)
            .HasConversion(new EnumToStringConverter<SemesterType>());
    }
}