using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;

namespace YourCorporation.Modules.JobSystem.Api.Database.Configurations
{
    internal sealed class JobOfferEntityTypeConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

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
