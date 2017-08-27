using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingApp.Services;

namespace VotingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VotingApiService _votingApiService;

        public string Name { get; private set; }
        public string OptionA { get; private set; } = "Dogs";
        public string OptionB { get; private set; } = "Cats";

        public IndexModel(VotingApiService votingApiService)
        {
            _votingApiService = votingApiService;
        }

        public IActionResult OnGet()
        {
            if(!Request.Cookies.ContainsKey(Constants.UserCookieKey))
            {
                return RedirectToPage("/Name");
            }

            var userCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key == Constants.UserCookieKey);
            Name = userCookie.Value;

            return Page();
                
        }
    }
}