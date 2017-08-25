using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VotingApp.Pages
{
    public class NameModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string name)
        {
            Response.Cookies.Append(Constants.UserCookieKey, name);
            
            return RedirectToPage("/Index");
        }
    }
}