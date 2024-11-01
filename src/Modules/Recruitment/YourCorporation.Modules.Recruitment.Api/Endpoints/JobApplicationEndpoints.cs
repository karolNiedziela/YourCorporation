using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Recruitment.Application.Features.JobApplications.AssignRecruiter;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Api.Endpoints
{
    internal class JobApplicationEndpoints : IMinimalApiEndpointDefinition
    {
        public const string AssignRecruiter = nameof(AssignRecruiter);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(RecruitmentModule.BasePath + "/jobapplications");

            group.MapPatch("{jobApplicationId:guid}/assign-recruiter", AssignRecruiterAsync)
                .WithName(AssignRecruiter)
               .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Assign recruiter"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok<AssignRecruiterResponse>, BadRequest<List<Error>>>> AssignRecruiterAsync(
            AssignRecruiterCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Ok<AssignRecruiterResponse>, BadRequest<List<Error>>>>(
                onSuccess: (AssignRecruiterResponse assignRecruiterResponse) => TypedResults.Ok(assignRecruiterResponse),
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }

    }
}
