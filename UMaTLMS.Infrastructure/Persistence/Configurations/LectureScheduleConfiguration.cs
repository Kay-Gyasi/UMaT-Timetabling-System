using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class LectureScheduleConfiguration : DatabaseConfiguration<LectureSchedule, int>
{
    public override void Configure(EntityTypeBuilder<LectureSchedule> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.DayOfWeek)
            .HasConversion(new EnumToStringConverter<DayOfWeek>());
    }
}
