using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.JobSystem.Api.Database
{
    internal class JobSystemDbContext(DbContextOptions<JobSystemDbContext> options) : YourCorporationDbContext<JobSystemDbContext>(options), IInboxDbSet, IOutboxDbSet
    {
        public const string SchemaName = "jobsystem";

        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<WorkLocation> WorkLocations { get; set; }

        public DbSet<OutboxMessage> Outbox { get; set; }

        public DbSet<InboxMessage> Inbox { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobSystemDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
