using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Attendees;
using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Entities;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.Configurations
{
    internal class DeclaredEventAttendeeEntityTypeConfiguration : IEntityTypeConfiguration<DeclaredEventAttendee>
    {
        public void Configure(EntityTypeBuilder<DeclaredEventAttendee> builder)
        {
            builder.ToTable("DeclaredEventAttendees");

            builder.HasKey(nameof(AttendeeId), nameof(EventId));

            builder.Property(x => x.AttendeeId)
                .HasConversion(
                 attendeeId => attendeeId.Value,
                 value => new AttendeeId(value));

            builder.Property(x => x.EventId)
                  .HasConversion(
                   eventId => eventId.Value,
                   value => new EventId(value));

            builder.HasOne<Event>()
                .WithMany(x => x.DeclaredAttendees)
                .HasForeignKey(x => x.EventId);

            builder.HasOne<Attendee>()
                .WithMany()
                .HasForeignKey(x => x.AttendeeId);
        }
    }
}
