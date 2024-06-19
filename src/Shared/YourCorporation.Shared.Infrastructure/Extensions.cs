using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json.Serialization;
using YourCorporation.Shared.Infrastructure.Auth;
using YourCorporation.Shared.Infrastructure.Contexts;
using YourCorporation.Shared.Infrastructure.Exceptions;
using YourCorporation.Shared.Infrastructure.Messaging;
using YourCorporation.Shared.Infrastructure.MinimalApis;
using YourCorporation.Shared.Infrastructure.Persistence;
using YourCorporation.Shared.Infrastructure.SupabaseFeatures;
using YourCorporation.Shared.Infrastructure.Swagger;

namespace YourCorporation.Shared.Infrastructure
{
    public static class Extensions
    {
        private const string CorrelationIdKey = "correlation-id";

        public static IServiceCollection AddModularInfrastructure(
            this IServiceCollection services, 
            IList<Assembly> assemblies,
            IConfiguration configuration)
        {
            services.AddSupabaseAuth(configuration);
            services.AddSupabaseFeatures(configuration);

            services.AddMemoryCache();

            services.AddSingleton(TimeProvider.System);

            services.AddExceptionHandling();

            services.AddSwaggerExtensions();

            services.AddContext();

            services.AddSqlServer(configuration);

            services.AddMessaging(configuration, [.. assemblies]);

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

            services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");

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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseExceptionHandling();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAntiforgery();

            app.UseContext();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuth();

            app.MapModuleEndpoints([.. assemblies]);

            return app;
        }

        public static string GetModuleName(this object value)
            => value?.GetType().GetModuleName() ?? string.Empty;

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

        public static Guid? TryGetCorrelationId(this HttpContext context)
            => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid)id : null;
    }
}