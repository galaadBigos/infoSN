using InfoSN.App_Code.Helpers;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories.Implementations
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

            command.ExecuteNonQuery();

            _dbConnection.Close();
        }

        public User? GetUser(string email)
        {
            User? result = null;
            string query = CRUDHelper.GenerateGetByQuery(_table, "email_user", email);

            _dbConnection.Open();

            IDbCommand command = _dbConnection.CreateCommand();
            command.CommandText = query;
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = UserHelper.GenerateUserFromDb(reader);
            }

            _dbConnection.Close();

            return result;
        }


    }
}
