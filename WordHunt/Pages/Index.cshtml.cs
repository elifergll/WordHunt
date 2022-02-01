using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Data;
using WordHunt.Services;

namespace WordHunt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MainService mainService;
        public WordInfo word;

        public IndexModel(ILogger<IndexModel> logger,MainService mainService)
        {
            _logger = logger;
            this.mainService = mainService;
        }
        public async Task OnGet()
        {
            word = await mainService.GetWordAsync(1);
        }
        public void OnPost()
        {

        }
    }
}
