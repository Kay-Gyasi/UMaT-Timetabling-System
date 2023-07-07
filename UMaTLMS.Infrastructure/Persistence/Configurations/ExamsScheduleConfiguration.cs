using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class ExamsScheduleConfiguration : DatabaseConfiguration<ExamsSchedule, int>
{
    public override void Configure(EntityTypeBuilder<ExamsSchedule> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ExamPeriod)
            .HasConversion(new EnumToStringConverter<ExamPeriod>());
    }
}