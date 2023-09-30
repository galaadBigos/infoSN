using InfoSN.App_Code.Helpers.Entities.Implementations;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Articles;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;

namespace InfoSN.Services.Implementations
{
	public class ArticleService : IArticleService
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IUserRepository _userRepository;

		public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
		{
			_articleRepository = articleRepository;
			_userRepository = userRepository;
		}

		public IEnumerable<DisplayArticleVM> GetAllArticles()
		{
			List<DisplayArticleVM> result = new List<DisplayArticleVM>();

			IEnumerable<Article> articles = _articleRepository.GetAllArticles();
			IEnumerable<User> users = _userRepository.GetAllUsers();

			foreach (Article article in articles)
			{
				User user = users.Where(u => u.Id == article.IdUser).FirstOrDefault()!;
				DisplayArticleVM model = ArticleHelpers.GenerateDisplayArticleVM(article, user);

				result.Add(model);
			}

			return result;
		}

		public DetailsArticleVM? GetDetailsArticleVM(string id)
		{
			Article? article = _articleRepository.GetArticle(id);

			if (article is null)
				return null;

			return ArticleHelpers.GenerateDetailsArticleVM(article);
		}

		public UpdateArticleVM? GetUpdateArticleVM(string id)
		{
			Article? article = _articleRepository.GetArticle(id);

			if (article is null)
				return null;

			return ArticleHelpers.GenerateUpdateArticleVM(article);
		}

		public void PostArticle(NewArticleVM model)
		{
			Article article = ArticleHelpers.CreateArticle(model);
			_articleRepository.PostArticle(article);
		}

		public void UpdateArticle(UpdateArticleVM model)
		{
			Article article = ArticleHelpers.GenerateArticleFromUpdateArticleVM(model);
			_articleRepository.UpdateArticle(article);
		}
	}
}
