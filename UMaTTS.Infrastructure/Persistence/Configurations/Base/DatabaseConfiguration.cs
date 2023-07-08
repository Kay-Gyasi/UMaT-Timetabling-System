using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UMaTLMS.Infrastructure.Persistence.Configurations.Base;

public class DatabaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Audit, a =>
        {
            a.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            a.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            a.Property(x => x.CreatedBy)
                .IsRequired()
                .HasDefaultValue("admin")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            a.Property(x => x.UpdatedBy)
                .IsRequired()
                .HasDefaultValue("admin");
            a.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<EntityStatus>());
        });
        builder.HasQueryFilter(x => x.Audit!.Status == EntityStatus.Normal);
    }
}
