using YourCorporation.Shared.Abstractions.Extensions;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public sealed class UnitOfWorkTypeRegistry
    {
        private readonly Dictionary<string, Type> _types = [];

        public void Register<T>() where T : IUnitOfWork => _types[GetKey<T>()] = typeof(T);

        public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

        public Type Resolve(Type resolveType) => _types.TryGetValue(GetKey(resolveType), out var type) ? type : null;

        private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";

        private static string GetKey(Type type)
            => type.IsGenericType
                ? $"{type.GenericTypeArguments[0].GetModuleName()}"
                : $"{type.GetModuleName()}";
    }
}
