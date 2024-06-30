using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class SpeakerEntityTypeConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.Id)
                  .HasConversion(
                   speakerId => speakerId.Value,
                   value => new SpeakerId(value));

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                  .HasConversion(
                       firstName => firstName.Value,
                       value => FirstName.Create(value).Value);

            builder.Property(x => x.LastName)
                .HasColumnName("LastName")
                  .HasConversion(
                       lastName => lastName.Value,
                       value => LastName.Create(value).Value);
        }
    }
}
