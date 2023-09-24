using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
	public interface IArticleRepository
	{
		public IEnumerable<Article> GetAllArticles();
		public Article? GetArticle(string id);
	}
}
