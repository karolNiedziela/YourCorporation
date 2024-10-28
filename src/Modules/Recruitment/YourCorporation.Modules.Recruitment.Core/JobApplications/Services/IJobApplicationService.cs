using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Services
{
    internal interface IJobApplicationService
    {
        Task<Result> ProcessNewJobApplication(JobApplicationId jobApplicationId, ContactId contactId);
    }
}
