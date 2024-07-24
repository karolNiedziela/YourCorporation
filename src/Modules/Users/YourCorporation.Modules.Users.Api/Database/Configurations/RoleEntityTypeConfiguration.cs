using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Users.Api.Entities;

namespace YourCorporation.Modules.Users.Api.Database.Configurations
{
    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity<RolePermission>(
                l => l.HasOne<Permission>().WithMany().HasForeignKey(x => x.PermissionId),
                r => r.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId));
        }
    }
}
