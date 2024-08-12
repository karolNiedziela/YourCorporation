using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.ContactStatuses;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactStatusEntityypeConfiguration : IEntityTypeConfiguration<ContactStatus>
    {
        public void Configure(EntityTypeBuilder<ContactStatus> builder)
        {
            builder.ToTable("ContactStatuses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .HasConversion(
              contactStatusId => contactStatusId.Value,
              value => new ContactStatusId(value));

            var contactStatuses = ContactStatus.GetAll();
            builder.HasData(contactStatuses);
        }
    }
}
