using InfoSN.App_Code.Helpers;
using InfoSN.App_Code.Helpers.Entities;
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
			string query = QueryHelpers.GenerateSecurePostQuery(user, _table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			QueryHelpers.AddParametersToDbCommand(command, user);

			if (command.ExecuteNonQuery() <= 0)
			{
				throw new Exception();
			}

			_dbConnection.Close();
		}

		public IEnumerable<User> GetAllUsers()
		{
			List<User> result = new List<User>();
			string query = QueryHelpers.GenerateGetAllQuery(_table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				User user = UserHelpers.GenerateUserFromDb(reader);
				result.Add(user);
			}

			return result;
		}

		public User? GetUser(string email)
		{
			User? result = null;
			string query = QueryHelpers.GenerateGetByQuery(_table, "email_user", email);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result = UserHelpers.GenerateUserFromDb(reader);
			}

			_dbConnection.Close();

			return result;
		}
	}
}
