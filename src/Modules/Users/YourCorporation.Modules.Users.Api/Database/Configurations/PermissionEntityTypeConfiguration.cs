using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Users.Api.Entities;

namespace YourCorporation.Modules.Users.Api.Database.Configurations
{
    internal class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
