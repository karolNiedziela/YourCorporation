using MassTransit;

namespace YourCorporation.Shared.Abstractions.Messaging
{
    public interface IMassTransitDefinition
    {
        IRabbitMqBusFactoryConfigurator ConfigureMassTransit(
            IBusRegistrationContext context, 
            IRabbitMqBusFactoryConfigurator configurator);

        IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator);
    }
}
