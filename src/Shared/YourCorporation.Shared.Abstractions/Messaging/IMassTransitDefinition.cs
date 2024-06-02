using MassTransit;

namespace YourCorporation.Shared.Abstractions.Messaging
{
    public interface IMassTransitDefinition
    {
        IRabbitMqBusFactoryConfigurator ConfigureRabbitMQ(
            IBusRegistrationContext busRegistrationContext,
            IRabbitMqBusFactoryConfigurator rabbitMQBusFactoryConfigurator);

        IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator);
    }
}
