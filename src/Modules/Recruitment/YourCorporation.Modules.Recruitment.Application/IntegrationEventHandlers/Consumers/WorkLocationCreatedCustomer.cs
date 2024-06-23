using MassTransit;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Consumers
{
    internal class WorkLocationCreatedCustomer : IConsumer<WorkLocationCreated>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public WorkLocationCreatedCustomer(IInboxCustomerHandler inboxHandler)
        {
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<WorkLocationCreated> context)
        {
            await _inboxHandler.Send(context, typeof(WorkLocationCreatedCustomer));
        }
    }
}
