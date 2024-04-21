using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json.Serialization;
using YourCorporation.Shared.Infrastructure.Swagger;
using YourCorporation.Shared.Infrastructure.MinimalApis;
using YourCorporation.Shared.Infrastructure.Persistence;
using YourCorporation.Shared.Infrastructure.Exceptions;
using FluentValidation;
using YourCorporation.Shared.Infrastructure.Messaging;
using MassTransit;
using YourCorporation.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Options;

namespace YourCorporation.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(
            this IServiceCollection services, 
            IList<Assembly> assemblies,
            IConfiguration configuration)
        {
            services.AddExceptionHandling();

            services.AddSwaggerExtensions();

            services.AddSqlServer(configuration);

            services.AddMessaging(configuration, assemblies.ToArray());

            services.AddEndpointsApiExplorer();

            services.AddProblemDetails();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            ValidatorOptions.Global.LanguageManager.Enabled = false;

            services.AddHostedService<DbContextInitializer>();

            return services;
        }

        public static IApplicationBuilder UseModularInfrastructure(this WebApplication app, IList<Assembly> assemblies)
        {
            app.UseExceptionHandling();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapModuleEndpoints([.. assemblies]);

            return app;
        }

        public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex]
                : string.Empty;
        }
    }
}