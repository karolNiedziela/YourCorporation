using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Modules.Forms.Api.Database
{
    internal class FormsDbContext : DbContext
    {
        public DbSet<EventForm> EventForms { get; set; }

        public DbSet<EventSubmission> EventSubmissions { get; set; }

        public DbSet<JobOfferForm> JobOfferForms { get; set; }

        public DbSet<JobOfferSubmission> JobOfferSubmissions { get; set; }

        public DbSet<WorkLocation> WorkLocations { get; set; }

        public DbSet<OutboxMessage> Outbox { get; set; }

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
