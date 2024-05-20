using MassTransit;
using YourCorporation.Modules.Forms.Api.Consumers;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Forms.Api
{
    internal class FormsMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureMassTransit(
            IBusRegistrationContext context, 
            IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint("event-live", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<EventWentLiveConsumer>(context);
            });

            configurator.ReceiveEndpoint("joboffer-publish", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<JobOfferPublishedCustomer>(context);
            });

            configurator.ReceiveEndpoint("worklocation-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<WorkLocationCreatedConsumer>(context);
            });

            configurator.ConfigureEndpoints(context);

            return configurator;
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
