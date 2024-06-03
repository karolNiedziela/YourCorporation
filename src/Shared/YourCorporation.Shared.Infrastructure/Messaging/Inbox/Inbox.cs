using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal class Inbox<T> : IInbox where T : DbContext
    {
        private readonly T _dbContext;
        private readonly DbSet<InboxMessage> _inboxMessages;
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<Inbox<T>> _logger;

        public bool Enabled { get; }

        public Inbox(T dbContext, TimeProvider timeProvider, ILogger<Inbox<T>> logger, IOptions<OutboxOptions> options)
        {
            _dbContext = dbContext;
            _inboxMessages = _dbContext.Set<InboxMessage>();
            _timeProvider = timeProvider;
            _logger = logger;
            Enabled = options.Value.Enabled;
        }

        public async Task HandleAsync(Guid messageId, string name, Func<Task> handler)
        {
            var module = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning($"Outbox is disabled ('{module}'), incoming messages won't be processed.");
                return;
            }

            _logger.LogTrace($"Received a message with Id: '{messageId}' to be processed ('{module}').");
            var isAlreadyProcessed = await _inboxMessages.AnyAsync(x => x.Id == messageId && x.ProcessedAt != null);
            if (isAlreadyProcessed)
            {
                _logger.LogTrace($"Message with Id: '{messageId}' was already processed ('{module}').");
                return;
            }

            _logger.LogTrace($"Processing a message with ID: '{messageId}' ('{module}')...");

            var inboxMessage = new InboxMessage
            {
                Id = messageId,
                Name = name,
                ReceivedAt = _timeProvider.GetUtcNow().DateTime
            };
            
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await handler();
                inboxMessage.ProcessedAt = _timeProvider.GetUtcNow().DateTime;
                await _inboxMessages.AddAsync(inboxMessage);
                await _dbContext.SaveChangesAsync();

                if (transaction is not null)
                {
                    await transaction.CommitAsync();
                }

                _logger.LogTrace($"Processed a message with Id: '{messageId}' ('{module}').");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There was an error when processing a message with Id: '{messageId}' ('{module}').");
                if (transaction is not null)
                {
                    await transaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                {
                    await transaction.DisposeAsync();
                }
            }
        }
    }
}
