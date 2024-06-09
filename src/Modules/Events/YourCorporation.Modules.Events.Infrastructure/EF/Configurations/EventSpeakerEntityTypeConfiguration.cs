using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Entities;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class EventSpeakerEntityTypeConfiguration : IEntityTypeConfiguration<EventSpeaker>
    {
        public void Configure(EntityTypeBuilder<EventSpeaker> builder)
        {
            builder.HasKey(nameof(SpeakerId), nameof(EventId));

            builder.Property(x => x.SpeakerId)
                .HasColumnName(nameof(SpeakerId))
                  .HasConversion(
                       speakerId => speakerId.Value,
                       value => new SpeakerId(value));

            builder.Property(x => x.EventId)
                .HasColumnName(nameof(EventId))
                  .HasConversion(
                       eventId => eventId.Value,
                       value => new EventId(value));

            builder.HasOne<Event>()
                .WithMany(x => x.Speakers)
                .HasForeignKey(x => x.EventId);

            builder.HasOne<Speaker>()
                .WithMany()
                .HasForeignKey(x => x.SpeakerId);
        }
    }
}
