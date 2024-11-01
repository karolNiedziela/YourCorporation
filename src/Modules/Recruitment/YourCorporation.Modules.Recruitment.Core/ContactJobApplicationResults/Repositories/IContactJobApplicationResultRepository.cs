using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Repositories
{
    internal interface IContactJobApplicationResultRepository
    {
        Task<ContactJobApplicationResult> GetAsync(JobApplicationId jobApplicationId);

        void Add(ContactJobApplicationResult contactJobApplicationResult);
    }
}
