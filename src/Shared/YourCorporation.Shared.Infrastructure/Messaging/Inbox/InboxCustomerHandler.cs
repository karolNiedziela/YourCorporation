using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal class InboxCustomerHandler : IInboxCustomerHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly InboxTypeRegistry _inboxTypeRegistry;
        private readonly bool _enabled;

        public InboxCustomerHandler(
            IServiceProvider serviceProvider,
            InboxTypeRegistry inboxTypeRegistry,
            IOptions<OutboxOptions> outboxOptions)
        {
            _serviceProvider = serviceProvider;
            _inboxTypeRegistry = inboxTypeRegistry;
            _enabled = outboxOptions.Value.Enabled;
        }

        public async Task Send(ConsumeContext<IMessage> context, Type consumerType, Func<Task> handler)
        {
            if (!_enabled)
            {
                return;
            }

            var inboxType = _inboxTypeRegistry.Resolve(consumerType);
            if (inboxType is null)
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var inbox = (IInbox)_serviceProvider.GetRequiredService(inboxType);
            var name = context.Message.GetType().Name;
            await inbox.HandleAsync(context.MessageId.Value, name, handler);
            
        }
    }
}
