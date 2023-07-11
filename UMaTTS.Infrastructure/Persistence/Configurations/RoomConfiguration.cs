namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : DatabaseConfiguration<ClassRoom, int>
{
    public override void Configure(EntityTypeBuilder<ClassRoom> builder)
    {
        base.Configure(builder);
        builder.ToTable(DomainEntities.Room);
        builder.HasIndex(x => x.Name);
    }
}