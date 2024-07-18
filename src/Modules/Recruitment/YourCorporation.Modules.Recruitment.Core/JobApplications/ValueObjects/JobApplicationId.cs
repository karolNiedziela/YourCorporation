using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal class JobApplicationId : StronglyTypedId
    {
        public JobApplicationId() : base()
        {
        }

        public JobApplicationId(Guid value) : base(value)
        {
        }
    }
}
