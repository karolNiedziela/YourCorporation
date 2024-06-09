using MassTransit;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Consumers
{
    internal class WorkLocationCreatedConsumer : IConsumer<WorkLocationCreated>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public WorkLocationCreatedConsumer(IInboxCustomerHandler inboxHandler)
        {
             _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<WorkLocationCreated> context)
        {
            await _inboxHandler.Send(context, typeof(WorkLocationCreatedConsumer));
        }
    }
}
