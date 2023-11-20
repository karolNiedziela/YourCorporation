using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.Forms
{
    internal sealed class EventFormEntityTypeConfiguration : FormBaseEntityTypeConfiguration<EventForm>
    {
        public override void Configure(EntityTypeBuilder<EventForm> builder)
        {
            builder.HasIndex(x => x.EventId).IsUnique();
            builder.Property(x => x.EventName).HasMaxLength(100);
            builder.Property(x => x.EventDescription).HasMaxLength(2000);

            builder.HasMany(x => x.Submissions)
                .WithOne(x => x.EventForm)
                .HasForeignKey(x => x.EventFormId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
