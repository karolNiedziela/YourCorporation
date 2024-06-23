using System.ComponentModel.DataAnnotations;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Constants
{
    internal enum JobApplicationStatus
    {
        [Display(Name = "Created")]
        Created,
        
        [Display(Name = "CV Verification")]
        CVVerification,

        [Display(Name = "Candidate Data Update")]
        CandiateDataUpdate,

        [Display(Name = "Communication")]
        Communication,
               
        [Display(Name = "Closed")]
        Closed
    }
}
