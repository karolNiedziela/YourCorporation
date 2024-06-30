using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Sessions;
using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(x => x.Id).IsClustered(false);
            builder.Property(x => x.Id)
                .HasConversion(
                    sessionId => sessionId.Value,
                    value => new SessionId(value));

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.EventId)
                .HasColumnName("EventId")
                .HasConversion(
                    eventId => eventId.Value,
                    value => new EventId(value));

            builder.Property(x => x.Name)
                  .HasColumnName("Name")
                  .HasConversion(
                   name => name.Value,
                   value => SessionName.Create(value).Value);

            builder.OwnsOne(x => x.BegginingAndEndOfSession, navigation =>
            {
                navigation.Property(x => x.StartTime)
                          .HasColumnName("StartTime");

                navigation.Property(x => x.EndTime)
                          .HasColumnName("EndTime");
            });

            builder.HasOne<Event>()
                   .WithMany()
                   .HasForeignKey(nameof(EventId));

            builder.Navigation(x => x.Speakers).AutoInclude();
        }
    }
}
