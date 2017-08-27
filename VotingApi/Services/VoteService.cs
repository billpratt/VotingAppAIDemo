using System.Threading.Tasks;
using VotingApi.Data.Repositories;
using VotingApi.Models;

namespace VotingApi.Services
{
    public class VoteService
    {
        private IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task Create(VoteModel voteModel)
        {
            await _voteRepository.Create(voteModel.VoterId, voteModel.Vote);
        }
    }
}
