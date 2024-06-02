using Microsoft.Extensions.Caching.Memory;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;

namespace YourCorporation.Shared.Infrastructure.Messaging.Contexts
{
    internal class MessageContextProvider : IMessageContextProvider
    {
        private readonly IMemoryCache _cache;

        public MessageContextProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IMessageContext Get(IMessage message) => _cache.Get<IMessageContext>(message);
    }
}
