using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.WorkLocations
{
    internal class WorkLocation : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public List<JobOfferForm> JobOfferForms { get; } = [];
        
        public List<JobOfferSubmission> JobOfferSubmissions { get; } = [];
    }
}
