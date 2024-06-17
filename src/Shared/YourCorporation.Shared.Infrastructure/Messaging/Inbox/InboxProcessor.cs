using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal sealed class InboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<InboxProcessor> _logger;
        private readonly bool _enabled;
        private readonly TimeSpan _interval;
        private readonly TimeSpan _startDelay;
        private int _isProcessing;

        public InboxProcessor(IServiceScopeFactory serviceScopeFactory,
            ILogger<InboxProcessor> logger, 
            IOptions<OutboxOptions> outboxOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _enabled = outboxOptions.Value.Enabled;
            _interval = outboxOptions.Value.Interval ?? TimeSpan.FromSeconds(1);
            _startDelay = outboxOptions.Value.StartDelay ?? TimeSpan.FromSeconds(5);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_enabled)
            {
                _logger.LogWarning("Inbox is disabled.");
                return;
            }

            _logger.LogInformation($"Inbox is enabled, start delay: {_startDelay}, interval: {_interval}");
            await Task.Delay(_startDelay, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogTrace("Started processing inbox messages...");
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                using var scope = _serviceScopeFactory.CreateScope();
                try
                {
                    var inboxes = scope.ServiceProvider.GetServices<IInbox>();
                    var tasks = inboxes.Select(inbox => inbox.ProcessUnprocessedAsync());
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
                    _logger.LogTrace($"Finished processing inbox messages in {stopwatch.ElapsedMilliseconds} ms.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
