using InfoSN.App_Code.Helpers;
using InfoSN.App_Code.Helpers.Entities;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories.Implementations
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly IDbConnection _dbConnection;
		private readonly TableNames _table;

		public ArticleRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
			_table = TableNames.Article;
		}

		public IEnumerable<Article> GetAllArticles()
		{
			List<Article> result = new List<Article>();
			string query = CRUDHelper.GenerateGetAllQuery(_table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Article article = ArticleHelper.GenerateArticleFromDb(reader);
				result.Add(article);
			}

			_dbConnection.Close();

			return result;
		}

		public Article? GetArticle(string id)
		{
			Article? result = null;
			string query = CRUDHelper.GenerateGetByQuery(_table, "id_article", id);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result = ArticleHelper.GenerateArticleFromDb(reader);
			}

			_dbConnection.Close();

			return result;
		}

		public void PostArticle(Article article)
		{
			string query = CRUDHelper.GenerateSecurePostQuery(article, _table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			CRUDHelper.AddParametersToDbCommand(command, article);

			command.ExecuteNonQuery();

			_dbConnection.Close();
		}
	}
}
