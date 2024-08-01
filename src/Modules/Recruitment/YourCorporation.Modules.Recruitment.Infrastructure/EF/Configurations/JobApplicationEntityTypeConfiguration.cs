using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Constants;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class JobApplicationEntityypeConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                jobApplicationId => jobApplicationId.Value,
                value => new JobApplicationId(value));

            builder.Property(x => x.JobApplicationStatus)
                   .HasConversion<EnumToStringConverter<JobApplicationStatus>>();

            builder.Property(x => x.ApplicationFirstName)
              .HasColumnName("ApplicationFirstName")
              .HasMaxLength(FirstName.MaxLength)
              .HasConversion(
              firstName => firstName.Value,
              value => FirstName.Create(value).Value);

            builder.Property(x => x.ApplicationLastName)
               .HasColumnName("ApplicationLastName")
               .HasMaxLength(LastName.MaxLength)
               .HasConversion(
               lastName => lastName.Value,
               value => LastName.Create(value).Value);

            builder.Property(x => x.ApplicationEmail)
                .HasColumnName("ApplicationEmail")
                .HasConversion(
                privateEmail => privateEmail.Value,
                value => PrivateEmail.Create(value).Value);

            builder.OwnsOne(x => x.JobOffer, navigation =>
            {
                navigation.Property(x => x.Id)
                    .HasColumnName("JobOfferId");

                navigation.Property(x => x.Name)
                    .HasColumnName("JobOfferName");
            });

            builder.Navigation(x => x.ChosenWorkLocations).AutoInclude();

            builder.HasOne<Contact>()
                .WithMany()
                .HasForeignKey(x => x.ContactId);
        }        
    }
}
