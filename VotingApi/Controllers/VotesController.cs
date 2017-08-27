using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApi.Models;

namespace VotingApi.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        // GET: api/votes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/votes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/votes
        [HttpPost]
        public IActionResult Post([FromBody]VoteModel voteModel)
        {
            return Ok();
        }
    }
}
