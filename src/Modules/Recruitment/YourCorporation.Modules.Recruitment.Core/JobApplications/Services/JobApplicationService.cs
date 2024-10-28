using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.Queues;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.Repositories;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Services
{
    internal sealed class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IRecruitmentQueueRepository _recruitmentQueueRepository;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository, IRecruitmentQueueRepository recruitmentQueueRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _recruitmentQueueRepository = recruitmentQueueRepository;
        }

        public async Task<Result> ProcessNewJobApplication(JobApplicationId jobApplicationId, ContactId contactId)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(jobApplicationId);
            if (jobApplication is null)
            {
                return ErrorCodes.JobApplications.NotFoundError(jobApplication.Id);
            }

            jobApplication.AssignContact(contactId);

            await AssignRecruitmentQueues(jobApplication);

            _jobApplicationRepository.Update(jobApplication);

            return Result.Success();
        }

        private async Task AssignRecruitmentQueues(JobApplication jobApplication)
        {
            var matchingRecruitmentQueues = await _recruitmentQueueRepository.FindMatchingQueuesAsync(jobApplication.ChosenWorkLocations);
            if (matchingRecruitmentQueues.Any())
            {
                jobApplication.AssignRecruitmentQueues([RecruitmentQueue.Any.Id]);
                return;
            }

            jobApplication.AssignRecruitmentQueues(matchingRecruitmentQueues);
        }
    }
}
