using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.Forms
{
    internal class JobOfferFormWorkLocationEntityTypeConfiguration : IEntityTypeConfiguration<JobOfferFormWorkLocation>
    {
        public void Configure(EntityTypeBuilder<JobOfferFormWorkLocation> builder)
        {
            builder.HasKey(x => new { x.JobOfferFormId, x.WorkLocationId }).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();         
        }
    }
}
