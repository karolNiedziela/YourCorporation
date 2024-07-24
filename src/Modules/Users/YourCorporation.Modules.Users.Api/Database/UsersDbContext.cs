using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using YourCorporation.Modules.Users.Api.Entities;

namespace YourCorporation.Modules.Users.Api.Database
{
    internal class UsersDbContext : DbContext
    {
        public const string SchemaName = "users";

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var primaryKey = entityType.FindPrimaryKey();

                if (primaryKey != null)
                {
                    modelBuilder.Entity(entityType.ClrType)
                                .HasKey(primaryKey.Properties.Select(p => p.Name).ToArray())
                                .IsClustered(false);
                }
                modelBuilder.Entity(entityType.Name).Property<int>("ClusterId").ValueGeneratedOnAdd();
                modelBuilder.Entity(entityType.Name).HasIndex("ClusterId").IsClustered(true);

                modelBuilder.Entity(entityType.Name).Property("ClusterId").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }
    }
}
