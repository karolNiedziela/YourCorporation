using Microsoft.AspNetCore.Routing;
using System.Reflection;
using YourCorporation.Shared.Abstractions.MinimalApis;

namespace YourCorporation.Shared.Infrastructure.MinimalApis
{
    internal static class Extensions
    {
        public static IEndpointRouteBuilder MapModuleEndpoints(
            this IEndpointRouteBuilder builder,
            params Assembly[] scanAssemblies)
        {
            var assemblies = scanAssemblies.Any() ? scanAssemblies : AppDomain.CurrentDomain.GetAssemblies();

            var endpoints = assemblies.SelectMany(x => x.GetTypes()).Where(t =>
                t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(IMinimalApiEndpointDefinition).IsAssignableFrom(t)).ToList();

            foreach (var endpoint in endpoints)
            {
                var instantiatedType = (IMinimalApiEndpointDefinition)Activator.CreateInstance(endpoint)!;
                instantiatedType.MapEndpoints(builder);
            }

            return builder;
        }
    }
}
