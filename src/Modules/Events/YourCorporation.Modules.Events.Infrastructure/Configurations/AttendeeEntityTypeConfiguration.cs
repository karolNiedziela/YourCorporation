using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Attendees;
using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Shared.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.Configurations
{
    internal class AttendeeEntityTypeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .HasConversion(
                  attendeeId => attendeeId.Value,
                  value => new AttendeeId(value));

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

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                  .HasConversion(
                       email => email.Value,
                       value => Email.Create(value).Value);
        }
    }
}
