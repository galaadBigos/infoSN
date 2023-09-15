using InfoSN.App_Code.Helpers;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly TableNames _table;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _table = TableNames.User;
        }

        public void PostUser(User user)
        {
            string query = CRUDHelper.GenerateSecurePostQuery(user, _table);

            _dbConnection.Open();

            IDbCommand command = _dbConnection.CreateCommand();
            command.CommandText = query;
            CRUDHelper.AddParametersToDbCommand(command, user);

            if (command.ExecuteNonQuery() <= 0)
            {
                throw new Exception("The insert query did not work");
            }

            _dbConnection.Close();
        }
    }
}
