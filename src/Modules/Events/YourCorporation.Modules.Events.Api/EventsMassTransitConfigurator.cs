using MassTransit;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Events.Api
{
    internal class EventsMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureRabbitMQ(
            IBusRegistrationContext busRegistrationContext,
            IRabbitMqBusFactoryConfigurator rabbitMQBusFactoryConfigurator)
        {            
            return rabbitMQBusFactoryConfigurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            return configurator;
        }
    }
}
