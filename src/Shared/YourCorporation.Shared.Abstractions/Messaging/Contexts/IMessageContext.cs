using YourCorporation.Shared.Abstractions.Contexts;

namespace YourCorporation.Shared.Abstractions.Messaging.Contexts
{
    public interface IMessageContext
    {
        public Guid MessageId { get; }

        public IContext Context { get; }
    }
}
