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
		private readonly TableNames _table;
		private readonly ArticleHelpers _helpers;

		public ArticleRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
			_table = TableNames.Article;
			_helpers = new ArticleHelpers();
		}

		public IEnumerable<Article> GetAllArticles()
		{
			List<Article> result = new List<Article>();
			string query = QueryHelpers.GenerateGetAllQuery(_table);

			result = QueryHelpers.GetAllEntities<Article>(_dbConnection, query, _helpers).ToList();

			return result;
		}

		public Article? GetArticle(string id)
		{
			Article? result = null;
			string query = QueryHelpers.GenerateGetByQuery(_table, "id_article", id);

			result = QueryHelpers.GetEntity<Article>(_dbConnection, query, _helpers);

			return result;
		}

		public void PostArticle(Article article)
		{
			string query = QueryHelpers.GenerateSecurePostQuery(article, _table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			QueryHelpers.AddParametersToDbCommand(command, article);

			if (command.ExecuteNonQuery() <= 0)
			{
				throw new Exception();
			}

			_dbConnection.Close();
		}
	}
}
