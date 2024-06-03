using MassTransit;
using YourCorporation.Modules.Forms.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Recruitment.IntegrationEvents.Consumers
{
    internal class JobOfferSubmissionCreatedConsumer : IConsumer<JobOfferSubmissionCreated>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public JobOfferSubmissionCreatedConsumer(IInboxCustomerHandler inboxHandler)
        {
            _inboxHandler = inboxHandler;
        }

        public Task Consume(ConsumeContext<JobOfferSubmissionCreated> context)
        {
            return Task.CompletedTask;
        }
    }
}
