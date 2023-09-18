using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;
internal class AdminSettingsConfiguration : DatabaseConfiguration<AdminSettings, int>
{
    public override void Configure(EntityTypeBuilder<AdminSettings> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Key)
            .HasConversion(new EnumToStringConverter<AdminConfigurationKeys>());
    }
}
