using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Contacts.Entities;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactStatusEntityypeConfiguration : IEntityTypeConfiguration<ContactStatus>
    {
        public void Configure(EntityTypeBuilder<ContactStatus> builder)
        {
            builder.ToTable("ContactStatuses");

            builder.HasKey(x => x.Id);

            var contactStatuses = ContactStatus.GetAll();
            builder.HasData(contactStatuses);
        }
    }
}
