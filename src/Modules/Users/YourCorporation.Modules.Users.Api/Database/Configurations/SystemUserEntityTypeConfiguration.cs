using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Users.Api.Entities;

namespace YourCorporation.Modules.Users.Api.Database.Configurations
{
    internal class SystemUserEntityTypeConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.SystemUsers)
                .UsingEntity<SystemUserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                    r => r.HasOne<SystemUser>().WithMany().HasForeignKey(x => x.SystemUserId));
        }
    }
}
