using System.ComponentModel.DataAnnotations;

namespace VotingApi.Models
{
    public class VoteModel
    {
        [Required]
        public string VoterId { get; set; }
        [Required]
        public string Vote { get; set; }
    }
}
