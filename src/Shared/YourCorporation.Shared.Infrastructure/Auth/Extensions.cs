using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
