﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _dbContext;
        private readonly IDomainEventsBroker _domainEventsBroker;

        public UnitOfWork(T dbContext, IDomainEventsBroker domainEventsBroker)
        {
            _dbContext = dbContext;
            _domainEventsBroker = domainEventsBroker;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = _dbContext.ChangeTracker.Entries<IAggregateRoot>()
           .Select(x => x.Entity)
           .SelectMany(aggregateRoot =>
           {
               var domainEvents = aggregateRoot.Events;
               aggregateRoot.ClearEvents();

               return domainEvents;
           })
           .ToArray();

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            await _domainEventsBroker.PublishAsync(domainEvents, cancellationToken);

            return result;
        }

        public async Task<int> SaveChangesFromNotificationHandlerAsync(IMessage sourceNotification, CancellationToken cancellationToken = default)
        {
            var domainEvents = _dbContext.ChangeTracker.Entries<IAggregateRoot>()
             .Select(x => x.Entity)
             .SelectMany(aggregateRoot =>
             {
                 var domainEvents = aggregateRoot.Events;
                 aggregateRoot.ClearEvents();

                 return domainEvents;
             })
             .ToArray();

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            await _domainEventsBroker.PublisFromNotificationHandlerAsync(sourceNotification, domainEvents, cancellationToken);

            return result;
        }
    }
}
