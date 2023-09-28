using InfoSN.App_Code.Helpers;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories.Implementations
{
	public class UserRoleRepository : IUserRoleRepository
	{
		private readonly IDbConnection _dbConnection;
		private readonly TableNames _table;

		public UserRoleRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
			_table = TableNames.UserRole;
		}

		public void PostUserRole(UserRole userRole)
		{
			string query = QueryHelpers.GenerateSecurePostQuery(userRole, _table);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			QueryHelpers.AddParametersToDbCommand(command, userRole);

			if (command.ExecuteNonQuery() <= 0)
			{
				throw new Exception();
			}

			_dbConnection.Close();
		}
	}
}
