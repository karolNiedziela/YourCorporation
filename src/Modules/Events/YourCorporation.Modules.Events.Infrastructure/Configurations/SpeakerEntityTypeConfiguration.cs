using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.Configurations
{
    internal class SpeakerEntityTypeConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .HasConversion(
                   speakerId => speakerId.Value,
                   value => new SpeakerId(value));

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                  .HasConversion(
                       firstName => firstName.Value,
                       value => new FirstName(value));

            builder.Property(x => x.LastName)
                .HasColumnName("LastName")
                  .HasConversion(
                       lastName => lastName.Value,
                       value => new LastName(value));
        }
    }
}
