using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class WorkLocationEntityypeConfiguration : IEntityTypeConfiguration<WorkLocation>
    {
        public const int MaxCodeLength = 6;

        public void Configure(EntityTypeBuilder<WorkLocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .HasConversion(
                  workLocationId => workLocationId.Value,
                  value => new WorkLocationId(value));


            builder.Property(x => x.Code).HasMaxLength(MaxCodeLength);
        }
    }
}
