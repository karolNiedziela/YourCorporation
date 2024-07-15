using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Modules.Recruitment.Infrastructure.EF.Configurations;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF
{
    internal class RecruitmentDbContext : DbContext, IInboxDbSet, IOutboxDbSet
    {
        public const string SchemaName = "recruitment";

        private readonly TimeProvider _timeProvider;

        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options, TimeProvider timeProvider)
            : base(options)
        {
            _timeProvider = timeProvider;
        }

        public DbSet<OutboxMessage> Outbox { get; set; }

        public DbSet<InboxMessage> Inbox { get; set; }

        public DbSet<WorkLocation> WorkLocations { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfiguration(new JobApplicationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration(_timeProvider));
            //modelBuilder.ApplyConfiguration(new ContactStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkLocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new JobApplicationChosenWorkLocationEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
