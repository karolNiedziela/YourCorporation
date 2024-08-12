using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Forms.Api.Database
{
    internal class FormsDbContext : YourCorporationDbContext<FormsDbContext>, IInboxDbSet, IOutboxDbSet
    {
        public const string SchemaName = "forms";

        public DbSet<EventForm> EventForms { get; set; }

        public DbSet<EventSubmission> EventSubmissions { get; set; }

        public DbSet<JobOfferForm> JobOfferForms { get; set; }

        public DbSet<JobOfferSubmission> JobOfferSubmissions { get; set; }

        public DbSet<WorkLocation> WorkLocations { get; set; }

        public DbSet<OutboxMessage> Outbox { get; set; }

        public DbSet<InboxMessage> Inbox { get; set; }

        public FormsDbContext(DbContextOptions<FormsDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
