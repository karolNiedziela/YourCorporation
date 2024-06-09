using MediatR;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Handlers
{
    internal class JobOfferPublishedHandler : INotificationHandler<JobOfferPublished>
    {
        private readonly IJobOfferFormRepository _jobOfferFormRepository;

        public JobOfferPublishedHandler(IJobOfferFormRepository jobOfferFormRepository)
        {
            _jobOfferFormRepository = jobOfferFormRepository;
        }

        public async Task Handle(JobOfferPublished notification, CancellationToken cancellationToken)
        {
            var jobOfferFormId = Guid.NewGuid();
            var jobOfferForm = new JobOfferForm(jobOfferFormId,
                notification.JobOfferId,
                notification.Name,
                notification.WorkLocationIds.Select(x => new JobOfferFormWorkLocation
                {
                    WorkLocationId = x,
                    JobOfferFormId = jobOfferFormId,
                }).ToList());

            await _jobOfferFormRepository.AddAsync(jobOfferForm);
        }
    }
}
