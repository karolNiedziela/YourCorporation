using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;

namespace YourCorporation.Modules.JobSystem.Api.Database.Configurations.JobOffers
{
    internal sealed class JobOfferEntityTypeConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.ToTable("JobOffers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.ToString(),
                    status => (JobOfferStatus)Enum.Parse(typeof(JobOfferStatus), status));

            builder.HasMany(x => x.WorkLocations)
                .WithMany()
                .UsingEntity("JobOfferWorkLocation");
        }
    }
}
