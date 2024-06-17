using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal class InboxCleanupProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<InboxCleanupProcessor> _logger;
        private readonly TimeSpan _interval;
        private readonly bool _enabled;
        private readonly TimeSpan _startDelay;
        private int _isProcessing;

        public InboxCleanupProcessor(IServiceScopeFactory serviceScopeFactory, IOptions<OutboxOptions> outboxOptions,
            ILogger<InboxCleanupProcessor> logger, TimeProvider timeProvider)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _enabled = outboxOptions.Value.Enabled;
            _interval = outboxOptions.Value.InboxCleanupInterval ?? TimeSpan.FromHours(1);
            _startDelay = outboxOptions.Value.StartDelay ?? TimeSpan.FromSeconds(5);
            _timeProvider = timeProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_enabled)
            {
                return;
            }

            await Task.Delay(_startDelay, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Interlocked.Exchange(ref _isProcessing, 1) == 1)
                {
                    await Task.Delay(_interval, stoppingToken);
                    continue;
                }

                _logger.LogTrace("Started cleaning up inbox messages...");
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var inboxes = scope.ServiceProvider.GetServices<IInbox>();
                        var tasks = inboxes.Select(inbox => inbox.CleanupAsync(_timeProvider.GetUtcNow().DateTime.Subtract(_interval)));
                        await Task.WhenAll(tasks);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError("There was an error when processing inbox.");
                        _logger.LogError(exception, exception.Message);
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _isProcessing, 0);
                        stopwatch.Stop();
                        _logger.LogTrace($"Finished cleaning up inbox messages in {stopwatch.ElapsedMilliseconds} ms.");
                    }
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
