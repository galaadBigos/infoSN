using InfoSN.Data.Constants;
using InfoSN.Models.ViewModel.Articles;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
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

		[HttpGet]
		[Authorize(Roles = RoleName.User)]
		public IActionResult Create()
		{
			NewArticleVM model = new NewArticleVM();
			model.IdUser = User.Claims.FirstOrDefault(c => c.Type == "UserId")!.Value;

			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = RoleName.User)]
		public IActionResult Create(NewArticleVM model)
		{
			if (ModelState.IsValid)
			{
				_articleService.PostArticle(model);

				return RedirectToAction("Home", "Index");
			}

			return View(model);
		}
	}
}
