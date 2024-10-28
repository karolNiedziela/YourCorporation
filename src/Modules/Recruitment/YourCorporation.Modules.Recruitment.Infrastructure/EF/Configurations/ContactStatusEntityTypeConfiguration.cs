using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.ContactStatuses;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactStatusEntityypeConfiguration : IEntityTypeConfiguration<ContactStatus>
    {
        public void Configure(EntityTypeBuilder<ContactStatus> builder)
        {
            builder.ToTable(nameof(ContactStatus));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .HasConversion(
              contactStatusId => contactStatusId.Value,
              value => new ContactStatusId(value));

            builder.Property(x => x.Status).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Substatus).HasMaxLength(100).IsRequired();

            var contactStatuses = ContactStatus.GetAll();
            builder.HasData(contactStatuses);
        }
    }
}
