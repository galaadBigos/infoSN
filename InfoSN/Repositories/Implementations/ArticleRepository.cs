using InfoSN.App_Code.Helpers;
using InfoSN.App_Code.Helpers.Entities.Implementations;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories.Implementations
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly IDbConnection _dbConnection;
		private readonly TableName _table;
		private readonly ArticleHelpers _helpers;

		public ArticleRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
			_table = TableName.Article;
			_helpers = new ArticleHelpers();
		}

		public IEnumerable<Article> GetAllArticles()
		{
			string query = QueryHelpers.GenerateGetAllQuery(_table);

			List<Article> result = QueryHelpers.GetAllEntities<Article>(_dbConnection, query, _helpers).ToList();

			return result;
		}

		public Article? GetArticle(string id)
		{
			string query = QueryHelpers.GenerateGetByQuery(_table, "id_article", id);

			Article? result = QueryHelpers.GetEntity<Article>(_dbConnection, query, _helpers);

			return result;
		}

		public void PostArticle(Article article)
		{
			string query = QueryHelpers.GenerateSecurePostQuery(article, _table);

			QueryHelpers.PostEntity(_dbConnection, query, article);
		}
	}
}
