using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordHunt.Data;
using WordHunt.Services;

namespace WordHunt.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private readonly AccountService accountService;

        public LoginModel(AccountService accountService)
        {
            this.accountService = accountService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var cnt = await accountService.AccountControlAsync(new UserInfo { UserName = Username, Password = Password } , HttpContext);
            if (cnt==true)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
