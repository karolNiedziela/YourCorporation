using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Candidates.Repositories
{
    internal interface ICandidateRepository
    {
        Task<Candidate?> GetAsync(PrivateEmail privateEmail);

        void Add(Candidate candidate);
    }
}
