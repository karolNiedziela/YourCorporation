using Microsoft.AspNetCore.Routing;

namespace YourCorporation.Shared.Abstractions.MinimalApis
{
    public interface IMinimalApiEndpointDefinition
    {
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder);
    }
}
