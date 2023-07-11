namespace UMaTLMS.Infrastructure.Persistence.Configurations
{
    public class ClassGroupConfiguration : DatabaseConfiguration<ClassGroup, int>
    {
        public override void Configure(EntityTypeBuilder<ClassGroup> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Name);
        }
    }
}