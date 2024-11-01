using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class ContactJobApplicationResultRepository : IContactJobApplicationResultRepository
    {
        private readonly DbSet<ContactJobApplicationResult> _contactJobApplicationResults;

        public ContactJobApplicationResultRepository(RecruitmentDbContext context)
        {
            _contactJobApplicationResults = context.ContactJobApplicationResults;
        }

        public async Task<ContactJobApplicationResult> GetAsync(JobApplicationId jobApplicationId)
            => await _contactJobApplicationResults.FirstOrDefaultAsync(x => x.JobApplicationId == jobApplicationId);

        public void Add(ContactJobApplicationResult contactJobApplicationResult)
            => _contactJobApplicationResults.Add(contactJobApplicationResult);

    }
}
