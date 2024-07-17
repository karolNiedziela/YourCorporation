using MediatR.Pipeline;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddSingleton(new UnitOfWorkTypeRegistry());

            return services;
        }

        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
        {
            services.AddTransient<IUnitOfWork, T>();
            services.AddTransient<T>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

            return services;
        }
    }
}
