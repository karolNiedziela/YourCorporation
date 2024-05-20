﻿using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Behaviors;

namespace YourCorporation.Modules.JobSystem.Api.Features
{
    internal static class Extensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            var applicationAssembly = typeof(JobOffersModule).Assembly;

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(applicationAssembly);
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

            return services;
        }
    }
}
