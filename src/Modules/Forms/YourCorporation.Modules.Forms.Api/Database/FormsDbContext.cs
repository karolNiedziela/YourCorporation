using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions;

namespace YourCorporation.Modules.Forms.Api.Database
{
    internal class FormsDbContext : DbContext
    {
        public DbSet<EventForm> EventForms { get; set; }

        public DbSet<EventSubmission> EventSubmissions { get; set; }
        
        public FormsDbContext(DbContextOptions<FormsDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("forms");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
