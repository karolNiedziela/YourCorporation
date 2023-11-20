using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.FormSubmissions
{
    internal abstract class FormSubmissionBaseEntityTypeConfiguration<TFormSubmission> : IEntityTypeConfiguration<TFormSubmission>
        where TFormSubmission : FormSubmissionBase
    {
        public virtual void Configure(EntityTypeBuilder<TFormSubmission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasMaxLength(100);

            builder.Property(x => x.LastName).HasMaxLength(100);

            builder.Property(x => x.Email).HasMaxLength(200);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
