using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supabase;
using YourCorporation.Shared.Infrastructure.Auth;

namespace YourCorporation.Shared.Infrastructure.SupabaseFeatures
{
    internal static class Extensions
    {
        public static IServiceCollection AddSupabaseFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SupabaseClientOptions>(configuration.GetSection(SupabaseClientOptions.SectionName));

            var supabaseClientOptions = configuration.GetSection(SupabaseClientOptions.SectionName).Get<SupabaseClientOptions>();

            services.AddScoped<Supabase.Client>(_ =>
            {
                return new Supabase.Client(
                    supabaseClientOptions.Url,
                    supabaseClientOptions.Key,
                    new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        AutoConnectRealtime = true,
                    }
                    );
            });

            return services;
        }
    }
}
