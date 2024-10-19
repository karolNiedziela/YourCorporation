using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YourCorporation.Shared.Abstractions.Extensions;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    internal class DbContextInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbContextInitializer> _logger;

        public DbContextInitializer(IServiceProvider serviceProvider, ILogger<DbContextInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext)
                {
                    continue;
                }

                _logger.LogInformation("Running migration for context for module {ModuleName}.", dbContextType.GetModuleName());
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
