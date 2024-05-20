using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.Forms
{
    internal sealed class JobOfferFormEntityTypeConfiguration : FormBaseEntityTypeConfiguration<JobOfferForm>
    {
        public override void Configure(EntityTypeBuilder<JobOfferForm> builder)
        {
            builder.HasIndex(x => x.JobOfferId).IsUnique();

            builder.HasMany(x => x.Submissions)
                .WithOne(x => x.JobOfferForm)
                .HasForeignKey(x => x.JobOfferFormId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.WorkLocations)
                .WithMany(x => x.JobOfferForms)
                .UsingEntity<JobOfferFormWorkLocation>(
                l => l.HasOne(x => x.WorkLocation).WithMany(x => x.JobOfferFormWorkLocations).HasForeignKey(x => x.WorkLocationId),
                r => r.HasOne(x => x.JobOfferForm).WithMany(x => x.JobOfferFormWorkLocations).HasForeignKey(x => x.JobOfferFormId));

            base.Configure(builder);
        }
    }
}
