﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.ContactStatuses;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class ContactEntityypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        private readonly TimeProvider _timeProvider;

        public ContactEntityypeConfiguration(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .HasConversion(
              contactId => contactId.Value,
              value => new ContactId(value));

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(FirstName.MaxLength)
                .HasConversion(
                firstName =>  firstName.Value,
                value => FirstName.Create(value).Value);

            builder.Property(x => x.LastName)
               .HasColumnName("LastName")
               .HasMaxLength(LastName.MaxLength)
               .HasConversion(
               lastName => lastName.Value,
               value => LastName.Create(value).Value);

            builder.Property(x => x.PrivateEmail)
                .HasColumnName("PrivateEmail")
                .HasMaxLength(100)
                .HasConversion(
                privateEmail => privateEmail.Value,
                value => PrivateEmail.Create(value).Value);

            builder.Property(x => x.PrivatePhone)
                .HasColumnName("PrivatePhone")
                .HasMaxLength(15)
                .HasConversion(
                privatePhone => privatePhone.Value,
                value => PrivatePhone.Create(value).Value);

            builder.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .HasConversion(
                birthDate => birthDate.Value,
                value => BirthDate.Create(value, _timeProvider).Value);

            builder.Property(x => x.LinkedinUrl)
                .HasColumnName("LinkedinUrl")
                .HasMaxLength(500)
                .HasConversion(
                linkedinUrl => linkedinUrl.Value,
                value => LinkedinUrl.Create(value).Value);


            builder.HasOne(x => x.ContactStatus)
                .WithMany()
                .HasForeignKey(nameof(ContactStatusId))
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
