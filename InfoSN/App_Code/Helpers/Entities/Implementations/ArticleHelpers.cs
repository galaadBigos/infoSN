using InfoSN.App_Code.Helpers.Entities.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.Entities.Abstractions;
using InfoSN.Models.ViewModel.Articles;
using System.Data;

namespace InfoSN.App_Code.Helpers.Entities.Implementations
{
	public class ArticleHelpers : EntityHelpers
	{
		public Article GenerateArticleFromDb(IDataReader reader)
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

		public static DetailsArticleVM GenerateDetailsArticleVM(Article article)
		{
			return new DetailsArticleVM()
			{
				Id = article.Id,
				Title = article.Title,
				Description = article.Description,
				PostDate = article.PostDate,
				EditDate = article.EditDate,
				IdUser = article.IdUser,
			};
		}

		public static UpdateArticleVM GenerateUpdateArticleVM(Article article)
		{
			return new UpdateArticleVM()
			{
				Id = article.Id,
				Title = article.Title,
				Description = article.Description,
				PostDate = article.PostDate,
				IdUser = article.IdUser,
			};
		}

		public static Article GenerateArticleFromUpdateArticleVM(UpdateArticleVM model)
		{
			return new Article()
			{
				Id = model.Id,
				Title = model.Title,
				Description = model.Description,
				PostDate = model.PostDate,
				EditDate = DateTime.Now,
				IdUser = model.IdUser,
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

		public override Entity GenerateEntityFromDb(IDataReader reader)
		{
			return GenerateArticleFromDb(reader);
		}
	}
}
