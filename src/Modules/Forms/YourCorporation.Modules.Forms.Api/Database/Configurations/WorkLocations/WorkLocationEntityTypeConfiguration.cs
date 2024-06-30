using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.WorkLocations
{
    internal class WorkLocationEntityTypeConfiguration : IEntityTypeConfiguration<WorkLocation>
    {
        public const int MaxCodeLength = 6;

        public void Configure(EntityTypeBuilder<WorkLocation> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.Code).HasMaxLength(MaxCodeLength);
        }
    }
}
