using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMaTLMS.Core.Enums;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class PreferenceConfiguration : DatabaseConfiguration<Preference, int>
{
    public override void Configure(EntityTypeBuilder<Preference> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Type)
            .HasConversion(new EnumToStringConverter<PreferenceType>());
        builder.Property(x => x.TimetableType)
            .HasConversion(new EnumToStringConverter<TimetableType>());
    }
}
