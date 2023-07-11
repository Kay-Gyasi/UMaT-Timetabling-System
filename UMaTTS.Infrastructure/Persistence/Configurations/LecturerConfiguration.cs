namespace UMaTLMS.Infrastructure.Persistence.Configurations
{
    public class LecturerConfiguration : DatabaseConfiguration<Lecturer, int>
    {
        public override void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.UmatId);
        }
    }
}