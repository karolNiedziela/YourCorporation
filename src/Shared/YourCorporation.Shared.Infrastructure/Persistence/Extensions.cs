using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public static class Extensions
    {
        internal static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MSSQLOptions>(configuration.GetSection(MSSQLOptions.SectionName));

            services.AddUnitOfWork();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddSingleton(new UnitOfWorkTypeRegistry());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWorkModuleContext
        {
            services.AddScoped<IUnitOfWorkModuleContext, T>();
            services.AddScoped<T>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

            return services;
        }
    }
}
