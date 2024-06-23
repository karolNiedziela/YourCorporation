using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Recruitment.Core.Candidates;
using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations
{
    internal class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        private readonly TimeProvider _timeProvider;

        public CandidateEntityTypeConfiguration(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                candidateId => candidateId.Value,
                value => new CandidateId(value));

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
                .HasConversion(
                privateEmail => privateEmail.Value,
                value => PrivateEmail.Create(value).Value);

            builder.Property(x => x.PrivatePhone)
                .HasColumnName("PrivatePhone")
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
                .HasConversion(
                linkedinUrl => linkedinUrl.Value,
                value => LinkedinUrl.Create(value).Value);
        }
    }
}
