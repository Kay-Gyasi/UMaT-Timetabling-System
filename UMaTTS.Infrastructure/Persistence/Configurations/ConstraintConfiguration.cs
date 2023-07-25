using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMaTLMS.Core.Enums;

namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class ConstraintConfiguration : DatabaseConfiguration<Constraint, int>
{
    public override void Configure(EntityTypeBuilder<Constraint> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Type)
            .HasConversion(new EnumToStringConverter<ConstraintType>());
        builder.Property(x => x.TimetableType)
            .HasConversion(new EnumToStringConverter<TimetableType>());
    }
}
