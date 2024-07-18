using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Infrastructure.Persistence;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF
{
    internal class RecruitmentUnitOfWork : UnitOfWorkModuleContext<RecruitmentDbContext>
    {
        public RecruitmentUnitOfWork(RecruitmentDbContext dbContext, IDomainEventsBroker domainEventsBroker) : base(dbContext, domainEventsBroker)
        {
        }
    }
}
