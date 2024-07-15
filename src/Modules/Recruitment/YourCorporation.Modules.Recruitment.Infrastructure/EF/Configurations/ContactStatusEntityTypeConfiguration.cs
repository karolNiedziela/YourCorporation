using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Contacts.Entities;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactStatusEntityTypeConfiguration : IEntityTypeConfiguration<ContactStatus>
    {
        public void Configure(EntityTypeBuilder<ContactStatus> builder)
        {
            builder.ToTable("ContactStatuses");

            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            var contactStatuses = ContactStatus.GetAll();
            builder.HasData(contactStatuses);
        }
    }
}
