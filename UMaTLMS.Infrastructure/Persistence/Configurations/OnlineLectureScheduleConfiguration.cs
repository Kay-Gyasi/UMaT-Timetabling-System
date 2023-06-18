using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class OnlineLectureScheduleConfiguration : DatabaseConfiguration<OnlineLectureSchedule, int>
{
    public override void Configure(EntityTypeBuilder<OnlineLectureSchedule> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.DayOfWeek)
            .HasConversion(new EnumToStringConverter<DayOfWeek>());
    }
}