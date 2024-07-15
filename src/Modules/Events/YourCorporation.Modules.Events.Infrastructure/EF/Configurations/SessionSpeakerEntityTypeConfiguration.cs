using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Events.Core.Sessions;
using YourCorporation.Modules.Events.Core.Sessions.Entities;
using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Configurations
{
    internal class SessionSpeakerEntityypeConfiguration : IEntityTypeConfiguration<SessionSpeaker>
    {
        public void Configure(EntityTypeBuilder<SessionSpeaker> builder)
        {
            builder.HasKey(nameof(SpeakerId), nameof(SessionId));

            builder.Property(x => x.SpeakerId)
                           .HasColumnName(nameof(SpeakerId))
                             .HasConversion(
                                  speakerId => speakerId.Value,
                                  value => new SpeakerId(value));

            builder.Property(x => x.SessionId)
                .HasColumnName(nameof(SessionId))
                  .HasConversion(
                       sessionId => sessionId.Value,
                       value => new SessionId(value));

            builder.HasOne<Session>()
                .WithMany(x => x.Speakers)
                .HasForeignKey(x => x.SessionId);

            builder.HasOne<Speaker>()
                .WithMany()
                .HasForeignKey(x => x.SpeakerId);
        }
    }
}
