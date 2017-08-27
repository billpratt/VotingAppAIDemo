using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VotingApi.Models;
using VotingApi.Services;
using System.Threading.Tasks;

namespace VotingApi.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private VoteService _voteService;

        public VotesController(VoteService voteService)
        {
            _voteService = voteService;
        }

        // POST api/votes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VoteModel voteModel)
        {
            await _voteService.Create(voteModel);

            return Ok();
        }
    }
}
