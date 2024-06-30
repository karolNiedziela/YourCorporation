using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourCorporation.Modules.Forms.Api.Entities.Forms;

namespace YourCorporation.Modules.Forms.Api.Database.Configurations.Forms
{
    internal abstract class FormBaseEntityTypeConfiguration<TForm> : IEntityTypeConfiguration<TForm> where TForm : FormBase
    {
        public virtual void Configure(EntityTypeBuilder<TForm> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);

            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.ClusterId)
                .IsUnique()
                .IsClustered();

            builder.Property(x => x.Name).HasMaxLength(108);

            builder.Property(x => x.IsUniqueSubmission).HasDefaultValue(false);
        }
    }
}
