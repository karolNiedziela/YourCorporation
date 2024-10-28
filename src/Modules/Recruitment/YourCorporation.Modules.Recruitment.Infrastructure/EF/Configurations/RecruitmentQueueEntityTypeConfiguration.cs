using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Queues;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class RecruitmentQueueEntityTypeConfiguration : IEntityTypeConfiguration<RecruitmentQueue>
    {
        public void Configure(EntityTypeBuilder<RecruitmentQueue> builder)
        {
            builder.ToTable(nameof(RecruitmentQueue));

           builder.HasKey(x => x.Id);

           builder.Property(x => x.Id)
               .HasConversion(
               recruitmentQueueId => recruitmentQueueId.Value,
               value => new RecruitmentQueueId(value));

            builder.ComplexProperty(x => x.Name)
               .IsRequired()
               .Property(n => n.Value)
               .HasColumnName("Name")
               .HasMaxLength(RecruitmentQueueName.MaxLength)
               .IsRequired();

            ConfigureWorkLocations(builder);
        }

        private void ConfigureWorkLocations(EntityTypeBuilder<RecruitmentQueue> builder)
        {
            builder.OwnsMany(x => x.WorkLocations, chosenWorkLocationBuilder =>
            {
                chosenWorkLocationBuilder.ToTable("RecruitmentQueueWorkLocation");
                chosenWorkLocationBuilder.WithOwner().HasForeignKey(nameof(RecruitmentQueueId));

                chosenWorkLocationBuilder.HasKey("Id");

                chosenWorkLocationBuilder.Property(wl => wl.Value)
                    .HasColumnName(nameof(WorkLocationId))
                    .ValueGeneratedNever();

                builder.Metadata.FindNavigation(nameof(RecruitmentQueue.WorkLocations))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
