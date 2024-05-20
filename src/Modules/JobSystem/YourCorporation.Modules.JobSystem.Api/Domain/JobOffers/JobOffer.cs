using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Shared.Abstractions.Results;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers
{
    internal class JobOffer : Entity<Guid>
    {
        private List<WorkLocation> _workLocations = [];

        public string Name { get; private set; }

        public JobOfferStatus Status { get; private set; }

        public IReadOnlyCollection<WorkLocation> WorkLocations => _workLocations.AsReadOnly();

        private JobOffer() { }

        public JobOffer(string name, List<WorkLocation> workLocations, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            Status = JobOfferStatus.New;
            _workLocations.AddRange(workLocations);
        }

        public Result Publish()
            => CheckAndChangeStatus(JobOfferStatus.New, JobOfferStatus.Published); 

        public Result Finish()
            => CheckAndChangeStatus([JobOfferStatus.New, JobOfferStatus.Published], JobOfferStatus.Finished);      
        
        private Result CheckAndChangeStatus(JobOfferStatus validStatusToMakeChange, JobOfferStatus newStatus)
        {
            if (Status != validStatusToMakeChange)
            {
                return ErrorCodes.JobOffers.InvalidStatusError(newStatus.ToString());
            }

            Status = newStatus;

            return Result.Success();
        }

        private Result CheckAndChangeStatus(List<JobOfferStatus> validStatusesToMakeChange, JobOfferStatus newStatus)
        {
            if (!validStatusesToMakeChange.Contains(Status))
            {
                return ErrorCodes.JobOffers.InvalidStatusError(newStatus.ToString());
            }

            Status = newStatus;

            return Result.Success();
        }
    }
}
