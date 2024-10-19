using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Outbox
{
    internal sealed class OutboxCleanupProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<OutboxCleanupProcessor> _logger;
        private readonly TimeSpan _cleanupInterval;
        private readonly bool _enabled;
        private readonly TimeSpan _startDelay;
        private int _isProcessing;

        public OutboxCleanupProcessor(
            IServiceScopeFactory serviceScopeFactory,
            TimeProvider timeProvider,
            ILogger<OutboxCleanupProcessor> logger,
            IOptions<OutboxOptions> outboxOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _timeProvider = timeProvider;
            _logger = logger;
            _cleanupInterval = outboxOptions.Value.CleanupInterval ?? TimeSpan.FromHours(1);
            _startDelay = outboxOptions.Value.StartDelay ?? TimeSpan.FromSeconds(5);
            _enabled = outboxOptions.Value.Enabled;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_enabled)
            {
                _logger.LogWarning("Outbox is disabled.");
                return;
            }

            await Task.Delay(_startDelay, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (Interlocked.Exchange(ref _isProcessing, 1) == 1)
                {
                    await Task.Delay(_cleanupInterval, stoppingToken);
                    continue;
                }

                _logger.LogTrace("Started cleaning up outbox messages...");
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                using (var scope = _serviceScopeFactory.CreateScope()) 
                {
                    try
                    {
                        var outboxes = scope.ServiceProvider.GetServices<IOutbox>();

                        var cleanupTo = _timeProvider.GetUtcNow().Date.Subtract(_cleanupInterval);
                        var tasks = outboxes.Select(outbox => outbox.CleanupAsync(cleanupTo));

                        await Task.WhenAll(tasks);
                    }
                    catch(Exception exception)
                    {
                        _logger.LogError("There was an error when processing outbox.");
                        _logger.LogError(exception, exception.Message);
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isProcessing, 0);
                        stopwatch.Stop();
                        _logger.LogTrace("Finished cleaning up outbox messages in {OutboxCleanupProcessorElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);
                    }
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }
    }
}
