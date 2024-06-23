using MassTransit;
using YourCorporation.Modules.Forms.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Consumers
{
    internal class JobOfferSubmissionCreatedConsumer : IConsumer<JobOfferSubmissionCreated>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public JobOfferSubmissionCreatedConsumer(IInboxCustomerHandler inboxHandler)
        {
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<JobOfferSubmissionCreated> context)
        {
            await _inboxHandler.Send(context, typeof(JobOfferSubmissionCreatedConsumer));
        }
    }
}
