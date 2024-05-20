using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.WorkLocations
{
    internal class WorkLocationEntityTypeConfiguration : IEntityTypeConfiguration<WorkLocation>
    {
        public void Configure(EntityTypeBuilder<WorkLocation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
