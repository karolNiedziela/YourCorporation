using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Enums;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.Id)
              .HasConversion(
              eventId => eventId.Value,
              value => new EventId(value));

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.Category)
                .HasConversion<EnumToStringConverter<EventCategory>>();

            builder.Property(x => x.Mode)
                .HasConversion<EnumToStringConverter<EventMode>>();

            builder.Property(x => x.Status)
                .HasConversion<EnumToStringConverter<EventStatus>>();

            builder.Property(x => x.Name)
               .HasColumnName("Name")
                  .HasConversion(
                   name => name.Value,
                   value => EventName.Create(value).Value);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasConversion(
                    description => description.Value,
                    value => EventDescription.Create(value).Value)
                .HasMaxLength(EventDescription.MaximimumLength);

            builder.OwnsOne(x => x.EventLimits, navigation =>
            {
                navigation.Property(eventLimits => eventLimits.AttendeesLimit)
                          .HasColumnName("AttendeesLimit");
            });

            builder.OwnsOne(x => x.BegginingAndEndOfEvent, navigation =>
            {
                navigation.Property(x => x.StartTime)
                          .HasColumnName("StartTime");

                navigation.Property(x => x.EndTime)
                          .HasColumnName("EndTime");
            });

            builder.Ignore(x => x.Speakers);
            builder.Ignore(x => x.WaitlistedAttendees);
            builder.Ignore(x => x.DeclaredAttendees);
            builder.Ignore(x => x.ConfirmedAttendees);

            //builder.Navigation(x => x.Speakers).AutoInclude();
            //builder.Navigation(x => x.WaitlistedAttendees).AutoInclude();
            //builder.Navigation(x => x.DeclaredAttendees).AutoInclude();
            //builder.Navigation(x => x.ConfirmedAttendees).AutoInclude();
        }
    }
}
