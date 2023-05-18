namespace UMaTLMS.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : DatabaseConfiguration<Room, int>
{
    public override void Configure(EntityTypeBuilder<Room> builder)
    {
        base.Configure(builder);
        builder.ToTable(DomainEntities.Room);
    }
}