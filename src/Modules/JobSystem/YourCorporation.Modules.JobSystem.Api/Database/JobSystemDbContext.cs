using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;

namespace YourCorporation.Modules.JobSystem.Api.Database
{
    internal class JobSystemDbContext(DbContextOptions<JobSystemDbContext> options) : DbContext(options)
    {
        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<WorkLocation> WorkLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("jobsystem");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobSystemDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
