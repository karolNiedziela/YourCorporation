using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Repositories;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal class JobOfferSubmissionRepository : IJobOfferSubmissionRepository
    {
        private readonly FormsDbContext _context;

        public JobOfferSubmissionRepository(FormsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobOfferSubmission jobOfferSubmission)
        {
            await _context.JobOfferSubmissions.AddAsync(jobOfferSubmission);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobOfferSubmission jobOfferSubmission)
        {
            _context.JobOfferSubmissions.Update(jobOfferSubmission);

            await _context.SaveChangesAsync();
        }
    }
}
