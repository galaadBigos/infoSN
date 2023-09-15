using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IDbConnection _dbConnection;

        public ArticleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Article> GetArticles()
        {
            throw new NotImplementedException();
        }
    }
}
