using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly DbSet<JobApplication> _jobApplications;

        public JobApplicationRepository(RecruitmentDbContext context)
        {
            _jobApplications = context.JobApplications;
        }

        public async Task<JobApplication?> GetAsync(Guid jobApplicationId)
            => await _jobApplications.FirstOrDefaultAsync(x => x.Id == jobApplicationId);

        public void Add(JobApplication jobApplication)
            => _jobApplications.Add(jobApplication);

        public void Update(JobApplication jobApplication)
            => _jobApplications.Update(jobApplication);
    }
}
