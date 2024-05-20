using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Constants
{
    internal enum JobApplicationStatus
    {
        [Display(Name = "CV Verification")]
        CVVerification,
        [Display(Name = "Duplicates Detection")]
        DuplicatesDection,
        [Display(Name = "Contact Registration/Update")]
        ContactRegistrationUpdate,
        [Display(Name = "Closing & Communication")]
        ClosingCommunication
    }
}
