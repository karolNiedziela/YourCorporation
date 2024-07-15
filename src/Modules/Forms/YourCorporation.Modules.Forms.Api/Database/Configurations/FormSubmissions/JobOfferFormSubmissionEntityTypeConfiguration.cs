using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal class JobOfferFormSubmissionEntityypeConfiguration : FormSubmissionBaseEntityypeConfiguration<JobOfferSubmission>
    {
        public override void Configure(EntityTypeBuilder<JobOfferSubmission> builder)
        {
            builder.HasMany(x => x.ChosenWorkLocations)
                .WithMany(x => x.JobOfferSubmissions)
                .UsingEntity<JobOfferSubmissionChosenWorkLocation>();

            base.Configure(builder);
        }
    }
}
