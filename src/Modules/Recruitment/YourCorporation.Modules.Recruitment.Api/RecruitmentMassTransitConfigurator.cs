using MassTransit;
using YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Consumers;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Recruitment.Api
{
    internal class RecruitmentMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureRabbitMQ(
            IBusRegistrationContext busRegistrationContext,
            IRabbitMqBusFactoryConfigurator rabbitMQBusFactoryConfigurator)
        {
            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("recruitment-joboffersubmission-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<JobOfferSubmissionCreatedConsumer>(busRegistrationContext);
            });

            rabbitMQBusFactoryConfigurator.ReceiveEndpoint("recruitment-worklocation-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<WorkLocationCreatedCustomer>(busRegistrationContext);
            });

            return rabbitMQBusFactoryConfigurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<JobOfferSubmissionCreatedConsumer>();

            configurator.AddConsumer<WorkLocationCreatedCustomer>();

            return configurator;
        }
    }
}
