using MassTransit;
using YourCorporation.Modules.Recruitment.IntegrationEvents.Consumers;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Recruitment.Api
{
    internal class RecruitmentMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureRabbitMQ(
            IBusRegistrationContext busRegistrationContext,
            IRabbitMqBusFactoryConfigurator rabbitMQBusFactoryConfigurator)
        {
            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("joboffersubmission-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<JobOfferSubmissionCreatedConsumer>(busRegistrationContext);
            });

            rabbitMQBusFactoryConfigurator.ConfigureEndpoints(busRegistrationContext);

            return rabbitMQBusFactoryConfigurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<JobOfferSubmissionCreatedConsumer>();

            return configurator;
        }
    }
}
