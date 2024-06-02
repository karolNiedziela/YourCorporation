using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Outbox
{
    internal sealed class OutboxBroker : IOutboxBroker
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly OutboxTypeRegistry _typeRegistry;

        public OutboxBroker(
            IServiceProvider serviceProvider,
            OutboxTypeRegistry typeRegistry)
        {
            _serviceProvider = serviceProvider;
            _typeRegistry = typeRegistry;
        }

        public async Task SendAsync(params IMessage[] messages)
        {
            var firstMessage = messages[0];
            var outboxType = _typeRegistry.Resolve(firstMessage);

            using var scope = _serviceProvider.CreateScope();
            var outbox = (IOutbox)scope.ServiceProvider.GetRequiredService(outboxType);
                   
            await outbox.SaveAsync(messages);
        }
    }
}
