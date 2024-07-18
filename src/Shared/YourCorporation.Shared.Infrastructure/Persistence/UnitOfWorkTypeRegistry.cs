using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public sealed class UnitOfWorkTypeRegistry
    {
        private readonly Dictionary<string, Type> _types = [];

        public void Register<T>() where T : IUnitOfWorkModuleContext => _types[GetKey<T>()] = typeof(T);

        public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

        public Type Resolve(IAggregateRoot aggregateRoot) => _types.TryGetValue(GetKey(aggregateRoot.GetType()), out var type) ? type : null;

        private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";

        private static string GetKey(Type type)
            => type.IsGenericType
                ? $"{type.GenericTypeArguments[0].GetModuleName()}"
                : $"{type.GetModuleName()}";
    }
}
