using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal sealed class EventSubmissionEntityTypeConfiguration : FormSubmissionBaseEntityTypeConfiguration<EventSubmission>
    {
        public override void Configure(EntityTypeBuilder<EventSubmission> builder)
        {
            base.Configure(builder);
        }
    }
}
