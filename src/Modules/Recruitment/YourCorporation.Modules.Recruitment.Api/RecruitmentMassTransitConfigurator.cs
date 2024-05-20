using MassTransit;
using YourCorporation.Modules.Recruitment.IntegrationEvents.Consumers;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Recruitment.Api
{
    internal class RecruitmentMassTransitConfigurator : IMassTransitDefinition
    {
        public IRabbitMqBusFactoryConfigurator ConfigureMassTransit(
            IBusRegistrationContext context, 
            IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint("joboffersubmission-created", x =>
            {
                x.PrefetchCount = 20;

                x.ConfigureConsumer<JobOfferSubmissionCreatedConsumer>(context);
            });

            configurator.ConfigureEndpoints(context);

            return configurator;
        }

        public IBusRegistrationConfigurator RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<JobOfferSubmissionCreatedConsumer>();

            return configurator;
        }
    }
}
