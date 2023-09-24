using InfoSN.Models.ViewModel.Articles;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace InfoSN.Controllers
{
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;

		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		[HttpGet]
		public IActionResult Details(string id)
		{
			DetailsArticleVM? model = _articleService.GetArticle(id);

			return View(model);
		}
	}
}
