using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Users.Api.Entities;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Users.Api.Database
{
    internal class UsersDbContext(DbContextOptions<UsersDbContext> options) : YourCorporationDbContext<UsersDbContext>(options)
    {
        public const string SchemaName = "users";

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
