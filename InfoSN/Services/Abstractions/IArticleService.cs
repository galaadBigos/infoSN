using InfoSN.Models.ViewModel.Articles;

namespace InfoSN.Services.Abstractions
{
	public interface IArticleService
	{
		public IEnumerable<DisplayArticleVM> GetAllArticles();
		public DetailsArticleVM? GetArticle(string id);
		public void PostArticle(NewArticleVM model);
	}
}
