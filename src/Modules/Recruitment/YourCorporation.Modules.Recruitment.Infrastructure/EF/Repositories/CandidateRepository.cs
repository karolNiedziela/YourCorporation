using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.Candidates;
using YourCorporation.Modules.Recruitment.Core.Candidates.Repositories;
using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class CandidateRepository : ICandidateRepository
    {
        private readonly DbSet<Candidate> _candidates;

        public CandidateRepository(RecruitmentDbContext context)
        {
            _candidates = context.Candidates;
        }

        public async Task<Candidate?> GetAsync(PrivateEmail privateEmail)
            => await _candidates.FirstOrDefaultAsync(x => x.PrivateEmail == privateEmail);

        public void Add(Candidate candidate)
            => _candidates.Add(candidate);
    }
}
