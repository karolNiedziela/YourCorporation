using YourCorporation.Shared.Abstractions.Contexts;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;

namespace YourCorporation.Shared.Infrastructure.Messaging.Contexts
{
    internal class MessageContext(Guid messageId, IContext context) : IMessageContext
    {
        public Guid MessageId { get; } = messageId;

        public IContext Context { get; } = context;
    }
}
