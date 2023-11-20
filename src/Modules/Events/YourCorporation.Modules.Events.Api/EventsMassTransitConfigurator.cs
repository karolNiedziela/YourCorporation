using MassTransit;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Events.Api
{
    internal class EventsMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureMassTransit(
            IBusRegistrationContext context, 
            IRabbitMqBusFactoryConfigurator configurator)
        {          
            return configurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            return configurator;
        }
    }
}
