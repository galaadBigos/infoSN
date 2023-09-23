using InfoSN.Models;
using InfoSN.Models.ViewModel.Articles;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InfoSN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            IEnumerable<DisplayArticleVM> model = _articleService.GetAllArticles();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}