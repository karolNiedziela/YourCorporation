using MassTransit;
using YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Consumers;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Forms.Api
{
    internal class FormsMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureRabbitMQ(
            IBusRegistrationContext busRegistrationContext,
            IRabbitMqBusFactoryConfigurator rabbitMQBusFactoryConfigurator)
        {           
            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("forms-event-live", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<EventWentLiveConsumer>(busRegistrationContext);
            });

            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("forms-joboffer-publish", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<JobOfferPublishedCustomer>(busRegistrationContext);
            });

            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("forms-worklocation-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<WorkLocationCreatedConsumer>(busRegistrationContext);
            });

            return rabbitMQBusFactoryConfigurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<EventWentLiveConsumer>();
            configurator.AddConsumer<JobOfferPublishedCustomer>();
            configurator.AddConsumer<WorkLocationCreatedConsumer>();

            return configurator;
        }
    }
}
