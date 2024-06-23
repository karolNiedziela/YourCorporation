namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories
{
    internal interface IJobApplicationRepository
    {
        Task<JobApplication> GetAsync(Guid jobApplicationId);

        void Add(JobApplication jobApplication);

        void Update(JobApplication jobApplication);
    }
}
