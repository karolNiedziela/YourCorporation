using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;

namespace YourCorporation.Modules.JobSystem.Api.Database.Configurations
{
    internal class JobOfferWorkLocationEntityypeConfiguration : IEntityTypeConfiguration<JobOfferWorkLocation>
    {
        public void Configure(EntityTypeBuilder<JobOfferWorkLocation> builder)
        {
            builder.HasKey(x => new { x.JobOfferId, x.WorkLocationId });
        }
    }
}
