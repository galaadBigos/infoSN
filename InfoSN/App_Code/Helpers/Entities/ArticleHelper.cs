using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Articles;
using System.Data;

namespace InfoSN.App_Code.Helpers.Entities
{
	public static class ArticleHelper
	{
		public static Article GenerateArticleFromDb(IDataReader reader)
		{
			return new Article()
			{
				Id = reader.GetString(0),
				Title = reader.GetString(1),
				Description = reader.GetString(2),
				PostDate = reader.GetDateTime(3),
				EditDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
				IdUser = reader.GetString(5),
			};
		}

		public static DisplayArticleVM GenerateDisplayArticleVM(Article article, User user)
		{
			return new DisplayArticleVM()
			{
				Id = article.Id,
				Title = article.Title,
				Description = article.Description,
				PostDate = article.PostDate,
				EditDate = article.EditDate,
				User = user
			};
		}

		public static DetailsArticleVM GenerateDisplayArticleVM(Article article)
		{
			return new DetailsArticleVM()
			{
				Title = article.Title,
				Description = article.Description,
				PostDate = article.PostDate,
				EditDate = article.EditDate,
				IdUser = article.IdUser,
			};
		}

		public static Article CreateArticle(NewArticleVM model)
		{
			return new Article()
			{
				Id = Guid.NewGuid().ToString(),
				Title = model.Title,
				Description = model.Description,
				PostDate = DateTime.Now,
				EditDate = null,
				IdUser = model.IdUser,
			};
		}
	}
}
