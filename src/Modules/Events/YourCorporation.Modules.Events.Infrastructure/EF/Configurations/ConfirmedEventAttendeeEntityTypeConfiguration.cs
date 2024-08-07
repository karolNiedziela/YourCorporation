﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Attendees;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Entities;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class ConfirmedEventAttendeeEntityypeConfiguration : IEntityTypeConfiguration<ConfirmedEventAttendee>
    {
        public void Configure(EntityTypeBuilder<ConfirmedEventAttendee> builder)
        {
            builder.ToTable("ConfirmedEventAttendees");

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
                .WithMany(x => x.ConfirmedAttendees)
                .HasForeignKey(x => x.EventId);

            builder.HasOne<Attendee>()
                .WithMany()
                .HasForeignKey(x => x.AttendeeId);
        }
    }
}
