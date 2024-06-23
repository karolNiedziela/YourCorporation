using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class JobApplicationChosenWorkLocationEntityTypeConfiguration : IEntityTypeConfiguration<JobApplicationChosenWorkLocation>
    {
        public void Configure(EntityTypeBuilder<JobApplicationChosenWorkLocation> builder)
        {
            builder.ToTable("JobApplicationChosenWorkLocations");
            builder.HasKey(nameof(JobApplicationId), nameof(WorkLocationId));

            builder.Property(x => x.JobApplicationId)
                .HasConversion(
                 jobApplicationId => jobApplicationId.Value,
                 value => new JobApplicationId(value));

            builder.Property(x => x.WorkLocationId)
                .HasConversion(
                 workLocationId => workLocationId.Value,
                 value => new WorkLocationId(value));

            builder.HasOne<JobApplication>().WithMany(x => x.ChosenWorkLocations).HasForeignKey(x => x.JobApplicationId).IsRequired();
            builder.HasOne<WorkLocation>().WithMany().HasForeignKey(x => x.WorkLocationId).IsRequired();

            builder.HasIndex(nameof(JobApplicationId), nameof(WorkLocationId));
        }
    }
}
