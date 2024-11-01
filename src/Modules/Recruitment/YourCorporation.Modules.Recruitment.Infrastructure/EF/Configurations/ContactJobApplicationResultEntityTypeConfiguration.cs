using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactJobApplicationResultEntityTypeConfiguration : IEntityTypeConfiguration<ContactJobApplicationResult>
    {
        public void Configure(EntityTypeBuilder<ContactJobApplicationResult> builder)
        {
            builder.ToTable(nameof(ContactJobApplicationResult));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                contactJobApplicationResultId => contactJobApplicationResultId.Value,
                value => new ContactJobApplicationResultId(value));

            builder.Property(x => x.JobApplicationId)
            .HasConversion(
            jobApplicationId => jobApplicationId.Value,
            value => new JobApplicationId(value))
            .IsRequired();

            builder.Property(x => x.ContactId)
                .HasConversion(
                contactId => contactId.Value,
                value => new ContactId(value))
                .IsRequired();

            builder.Property(x => x.ApplicationDecision)
               .HasConversion<EnumToStringConverter<ApplicationDecision>>()
               .IsRequired();

            builder.Property(x => x.RejectedReason)
                .HasConversion<EnumToStringConverter<RejectedReason>>()
                .IsRequired(false);


            builder.HasIndex(x => x.JobApplicationId);
        }
    }
}
