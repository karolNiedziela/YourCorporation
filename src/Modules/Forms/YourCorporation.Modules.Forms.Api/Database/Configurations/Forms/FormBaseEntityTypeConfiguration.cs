using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.Forms;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.Forms
{
    internal abstract class FormBaseEntityypeConfiguration<TForm> : IEntityTypeConfiguration<TForm> where TForm : FormBase
    {
        public virtual void Configure(EntityTypeBuilder<TForm> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(108);

            builder.Property(x => x.IsUniqueSubmission).HasDefaultValue(false);
        }
    }
}
