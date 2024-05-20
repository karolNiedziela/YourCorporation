using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories;

namespace YourCorporation.Modules.JobSystem.Api.Database.Repositories
{
    internal class JobOfferRepository : IJobOfferRepository
    {
        private readonly JobSystemDbContext _context;

        public JobOfferRepository(JobSystemDbContext context)
        {
            _context = context;
        }

        public async Task<JobOffer> GetAsync(Guid jobOfferId) 
            => await _context.JobOffers
            .Include(x => x.WorkLocations)
            .FirstOrDefaultAsync(x => x.Id == jobOfferId);

        public async Task AddAsync(JobOffer jobOffer)
        {
            await _context.JobOffers.AddAsync(jobOffer);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobOffer jobOffer)
        {
            _context.JobOffers.Update(jobOffer);

            await _context.SaveChangesAsync();
        }
    }
}
