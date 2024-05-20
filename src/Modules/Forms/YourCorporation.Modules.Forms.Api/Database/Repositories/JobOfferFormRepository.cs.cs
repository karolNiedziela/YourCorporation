using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal class JobOfferFormRepository : IJobOfferFormRepository
    {
        private readonly FormsDbContext _context;

        public JobOfferFormRepository(FormsDbContext context)
        {
            _context = context;
        }

        public async Task<JobOfferForm> GetAsync(Guid id)
            => await _context.JobOfferForms
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(JobOfferForm form)
        {
            await _context.JobOfferForms.AddAsync(form);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobOfferForm form)
        {
            _context.JobOfferForms.Update(form);

            await _context.SaveChangesAsync();
        }
    }
}
