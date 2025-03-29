
namespace DataAccess.Data.Configurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public  void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(Entity => Entity.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(Entity => Entity.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
