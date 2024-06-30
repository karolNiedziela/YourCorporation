using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Constants;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class JobApplicationEntityTypeConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.Id)
                .HasConversion(
                jobApplicationId => jobApplicationId.Value,
                value => new JobApplicationId(value));

            builder.Property(x => x.JobApplicationStatus)
                   .HasConversion<EnumToStringConverter<JobApplicationStatus>>();

            builder.OwnsOne(x => x.JobOffer, navigation =>
            {
                navigation.Property(x => x.Id)
                    .HasColumnName("JobOfferId");

                navigation.Property(x => x.Name)
                    .HasColumnName("JobOfferName");
            });

            builder.Property(x => x.CandidateId)
                .HasConversion(
                    candidateId => candidateId.Value,
                    value => new CandidateId(value));

            builder.Navigation(x => x.ChosenWorkLocations).AutoInclude();
        }        
    }
}
