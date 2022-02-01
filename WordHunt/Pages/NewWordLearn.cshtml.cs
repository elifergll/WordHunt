using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordHunt.Data;
using WordHunt.Services;

namespace WordHunt.Pages
{
    public class NewWordLearnModel : PageModel
    {
        [BindProperty]
        public List<Question> Questions { get; set; }
        public MainService MainService { get; }
        public NewWordLearnModel(MainService mainService)
        {
            MainService = mainService;
        }
        public async Task OnGet()
        {
            Questions = await MainService.GetQuestionAsync();
        }
        public async Task OnPost()
        {
            await Task.CompletedTask;
            var a = 0;
        }
    }
}
