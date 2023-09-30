using InfoSN.Models.ViewModel.Articles;

namespace InfoSN.Services.Abstractions
{
	public interface IArticleService
	{
		public IEnumerable<DisplayArticleVM> GetAllArticles();
		public DetailsArticleVM? GetDetailsArticleVM(string id);
		public UpdateArticleVM? GetUpdateArticleVM(string id);
		public void PostArticle(NewArticleVM model);
		public void UpdateArticle(UpdateArticleVM model);
	}
}
