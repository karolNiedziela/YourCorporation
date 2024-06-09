using MassTransit;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Consumers
{
    internal class JobOfferPublishedCustomer : IConsumer<JobOfferPublished>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public JobOfferPublishedCustomer(IInboxCustomerHandler inboxHandler)
        {
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<JobOfferPublished> context)
        {           
            await _inboxHandler.Send(context, typeof(JobOfferPublishedCustomer));
        }
    }
}
