using MassTransit;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class JobOfferPublishedCustomer : IConsumer<JobOfferPublished>
    {
        private readonly IJobOfferFormRepository _jobOfferFormRepository;
        private readonly IInboxCustomerHandler _inboxHandler;

        public JobOfferPublishedCustomer(IJobOfferFormRepository jobOfferFormRepository, IInboxCustomerHandler inboxHandler)
        {
            _jobOfferFormRepository = jobOfferFormRepository;
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<JobOfferPublished> context)
        {
            var jobOfferFormId = Guid.NewGuid();
            var jobOfferForm = new JobOfferForm(jobOfferFormId,
                context.Message.JobOfferId,
                context.Message.Name,
                context.Message.WorkLocationIds.Select(x => new JobOfferFormWorkLocation
                {
                    WorkLocationId = x,
                    JobOfferFormId = jobOfferFormId,
                }).ToList());

            await _inboxHandler.Send(
                context, 
                typeof(JobOfferPublishedCustomer),
                () => _jobOfferFormRepository.AddAsync(jobOfferForm));
        }
    }
}
