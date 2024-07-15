using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;

namespace YourCorporation.Modules.JobSystem.Api.Database.Configurations
{
    internal class WorkLocationEntityypeConfiguration : IEntityTypeConfiguration<WorkLocation>
    {
        public void Configure(EntityTypeBuilder<WorkLocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasConversion(
                    code => code.Value,
                    value => WorkLocationCode.Create(value).Value);
        }
    }
}
