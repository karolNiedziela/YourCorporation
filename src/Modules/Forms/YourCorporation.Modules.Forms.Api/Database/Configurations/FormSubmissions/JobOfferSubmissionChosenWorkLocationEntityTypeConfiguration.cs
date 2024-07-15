using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal class JobOfferSubmissionChosenWorkLocationEntityypeConfiguration : IEntityTypeConfiguration<JobOfferSubmissionChosenWorkLocation>
    {
        public void Configure(EntityTypeBuilder<JobOfferSubmissionChosenWorkLocation> builder)
        {
            builder.HasKey(x => new { x.JobOfferSubmissionId, x.WorkLocationId });             
        }
    }
}
