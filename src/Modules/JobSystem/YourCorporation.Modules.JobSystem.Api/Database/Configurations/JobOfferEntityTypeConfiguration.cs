using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;

namespace YourCorporation.Modules.JobSystem.Api.Database.Configurations
{
    internal sealed class JobOfferEntityypeConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.ToString(),
                    status => (JobOfferStatus)Enum.Parse(typeof(JobOfferStatus), status));

            builder.HasMany(x => x.WorkLocations)
                .WithMany(x => x.JobOffers)
                .UsingEntity<JobOfferWorkLocation>();
        }
    }
}
