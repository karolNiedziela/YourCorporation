using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal class JobOfferSubmissionChosenWorkLocationEntityTypeConfiguration : IEntityTypeConfiguration<JobOfferSubmissionChosenWorkLocation>
    {
        public void Configure(EntityTypeBuilder<JobOfferSubmissionChosenWorkLocation> builder)
        {
            builder.HasKey(x => new { x.JobOfferSubmissionId, x.WorkLocationId }).IsClustered(false);
            
            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();         
        }
    }
}
