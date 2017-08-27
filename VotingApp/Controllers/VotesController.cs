using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {        
        private readonly VotingApiService _votingApiService;

        public VotesController(VotingApiService votingApiService)
        {
            _votingApiService = votingApiService;
        }

        [HttpPost("{vote}")]
        public async Task<IActionResult> Post(string vote)
        {
            var userCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key == Constants.UserCookieKey);
            var result = await _votingApiService.PostVote(userCookie.Value, vote);

            var response = new
            {
                success = result.success,
                msg = result.msg
            };

            return Ok(response);
        }
    }
}
