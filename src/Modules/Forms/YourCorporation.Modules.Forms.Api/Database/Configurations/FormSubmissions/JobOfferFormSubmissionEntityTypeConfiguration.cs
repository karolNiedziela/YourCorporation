using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal class JobOfferFormSubmissionEntityTypeConfiguration : FormSubmissionBaseEntityTypeConfiguration<JobOfferSubmission>
    {
        public override void Configure(EntityTypeBuilder<JobOfferSubmission> builder)
        {
            builder.HasMany(x => x.ChosenWorkLocations)
                .WithMany(x => x.JobOfferSubmissions)
                .UsingEntity<JobOfferSubmissionChosenWorkLocation>(
                l => l.HasOne(x => x.WorkLocation).WithMany(x => x.JobOfferSubmissionChosenWorkLocations).HasForeignKey(x => x.WorkLocationId),
                r => r.HasOne(x => x.JobOfferSubmission).WithMany(x => x.JobOfferSubmissionChosenWorkLocations).HasForeignKey(x => x.JobOfferSubmissionId));

            base.Configure(builder);
        }
    }
}
