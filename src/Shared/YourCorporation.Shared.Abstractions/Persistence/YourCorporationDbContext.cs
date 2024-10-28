using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public abstract class YourCorporationDbContext<T> : DbContext where T : DbContext
    {
        protected YourCorporationDbContext(DbContextOptions<T> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.IsOwned())
                {
                    continue;
                }

                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey is null)
                {
                    continue;
                }


                // If using value object, Value Object for Id must contain Guid property with name Value
                bool isGuidKey = primaryKey.Properties.All(p =>
                {
                    var propertyType = p.ClrType;
                    return propertyType == typeof(Guid) ||
                           (propertyType.GetProperty("Value").PropertyType == typeof(Guid));
                });

                if (!isGuidKey)
                {
                    continue;
                }

                modelBuilder.Entity(entityType.ClrType)
                    .HasKey(primaryKey.Properties.Select(p => p.Name).ToArray())
                    .IsClustered(false);

                modelBuilder.Entity(entityType.Name).Property<int>(DbConstants.ClusterIdColumnName).ValueGeneratedOnAdd();
                modelBuilder.Entity(entityType.Name).HasIndex(DbConstants.ClusterIdColumnName).IsClustered(true);

                modelBuilder.Entity(entityType.Name).Property(DbConstants.ClusterIdColumnName).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
