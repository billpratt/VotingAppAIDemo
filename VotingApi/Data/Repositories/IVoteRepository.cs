using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApi.Data.Repositories
{
    public interface IVoteRepository
    {
        Task Create(string voterId, string vote);
    }
}
