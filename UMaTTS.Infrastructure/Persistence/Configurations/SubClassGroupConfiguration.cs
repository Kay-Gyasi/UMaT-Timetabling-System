namespace UMaTLMS.Infrastructure.Persistence.Configurations
{
    public class SubClassGroupConfiguration : DatabaseConfiguration<SubClassGroup, int>
    {
        public override void Configure(EntityTypeBuilder<SubClassGroup> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Name);
        }
    }
}