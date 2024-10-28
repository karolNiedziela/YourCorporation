using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class JobApplicationEntityypeConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.ToTable(nameof(JobApplication));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                jobApplicationId => jobApplicationId.Value,
                value => new JobApplicationId(value));

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CVUrl).HasMaxLength(500).IsRequired();
            builder.Property(x => x.JobOfferId).IsRequired();
            builder.Property(x => x.JobOfferSubmissionId).IsRequired();

            ConfigureChosenWorkLocations(builder);
            ConfigureRecruitmentQueues(builder);

            builder.HasOne<Contact>()
                .WithMany()
                .HasForeignKey(x => x.ContactId);
        }
        
        private void ConfigureChosenWorkLocations(EntityTypeBuilder<JobApplication> builder)
        {
            builder.OwnsMany(x => x.ChosenWorkLocations, chosenWorkLocationBuilder =>
            {
                chosenWorkLocationBuilder.ToTable("JobApplicationChosenWorkLocation");
                chosenWorkLocationBuilder.WithOwner().HasForeignKey(nameof(JobApplicationId));

                chosenWorkLocationBuilder.HasKey("Id");

                chosenWorkLocationBuilder.Property(wl => wl.Value)
                    .HasColumnName(nameof(WorkLocationId))
                    .ValueGeneratedNever();

                builder.Metadata.FindNavigation(nameof(JobApplication.ChosenWorkLocations))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });
        }

        private void ConfigureRecruitmentQueues(EntityTypeBuilder<JobApplication> builder)
        {
            builder.OwnsMany(x => x.RecruitmentQueues, chosenWorkLocationBuilder =>
            {
                chosenWorkLocationBuilder.ToTable("JobApplicationRecruitmentQueue");
                chosenWorkLocationBuilder.WithOwner().HasForeignKey(nameof(JobApplicationId));

                chosenWorkLocationBuilder.HasKey("Id");

                chosenWorkLocationBuilder.Property(wl => wl.Value)
                    .HasColumnName(nameof(RecruitmentQueueId))
                    .ValueGeneratedNever();

                builder.Metadata.FindNavigation(nameof(JobApplication.RecruitmentQueues))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
