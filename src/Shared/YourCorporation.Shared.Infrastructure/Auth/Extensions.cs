using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YourCorporation.Shared.Abstractions.Auth;

namespace YourCorporation.Shared.Infrastructure.Auth
{
    internal static class Extensions
    {
        public static IServiceCollection AddSupabaseAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddAuthorization();

            services.Configure<SupabaseAuthenticationOptions>(configuration.GetSection(SupabaseAuthenticationOptions.SectionName));

            var supabaseAuthenticationOptions = configuration.GetSection(SupabaseAuthenticationOptions.SectionName).Get<SupabaseAuthenticationOptions>();

            var bytes = Encoding.UTF8.GetBytes(supabaseAuthenticationOptions.JwtSecret);

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(bytes),
                    ValidAudience = supabaseAuthenticationOptions.ValidAudience,
                    ValidIssuer = supabaseAuthenticationOptions.ValidIssuer,
                };
            });

            return services;
        }

        public static IServiceCollection AddKeycloakAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddAuthorization();

            services.Configure<KeycloakOptions>(configuration.GetSection(KeycloakOptions.SectionName));

            var keycloakOptions = configuration.GetSection(KeycloakOptions.SectionName).Get<KeycloakOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = keycloakOptions.Authority;
                    options.Audience = keycloakOptions.Audience;
                    options.MetadataAddress = keycloakOptions.MetadataAddress;

                    var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
                    options.RequireHttpsMetadata = !isDevelopment;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = keycloakOptions.ValidAudience,
                        ValidateIssuer = true,
                        ValidIssuer = keycloakOptions.ValidIssuer,
                        ValidateLifetime = true,
                    };
                });

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
