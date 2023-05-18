namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class ClassConfiguration : DatabaseConfiguration<Class, int>
{
    public override void Configure(EntityTypeBuilder<Class> builder)
    {
        base.Configure(builder);
        builder.ToTable(DomainEntities.Class);
    }
}