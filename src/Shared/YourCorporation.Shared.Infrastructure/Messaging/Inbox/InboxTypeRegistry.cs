using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    public sealed class InboxTypeRegistry
    {
        private readonly Dictionary<string, Type> _types = [];

        public void Register<T>() where T : IInbox => _types[GetKey<T>()] = typeof(T);

        public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

        public Type Resolve(Type type) => _types.TryGetValue(GetKey(type), out type) ? type : null;

        private static string GetKey<T>() => GetKey(typeof(T));

        private static string GetKey(Type type)
            => type.IsGenericType
                ? $"{type.GenericTypeArguments[0].GetModuleName()}"
                : $"{type.GetModuleName()}";
    }
}
