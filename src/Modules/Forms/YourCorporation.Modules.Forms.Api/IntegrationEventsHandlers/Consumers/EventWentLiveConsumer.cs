using MassTransit;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Consumers
{
    internal class EventWentLiveConsumer : IConsumer<EventWentLive>
    {
        private readonly IInboxCustomerHandler _inboxHandler;

        public EventWentLiveConsumer(IInboxCustomerHandler inboxHandler)
        {;
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<EventWentLive> context)
        {
            await _inboxHandler.Send(
                context,
                typeof(EventWentLiveConsumer));
        }
    }
}
