using InfoSN.App_Code.Helpers.Entities;
using InfoSN.Models.Entities;
using InfoSN.Repositories.Abstractions;
using System.Data;

namespace InfoSN.Repositories.Implementations
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IDbConnection _dbConnection;
		private readonly TableNames _table;

		public RoleRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
			_table = TableNames.Role;
		}

		public IEnumerable<Role> GetRoles(string userId)
		{
			List<Role> result = new List<Role>();
			string query =
				$"SELECT * FROM [{_table}] " +
				$"INNER JOIN [RoleUser] ON [{_table}].id_role = [RoleUser].id_role " +
				$"WHERE [RoleUser].id_user = '{userId}'";

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Role role = RoleHelper.GenerateRoleFromDb(reader);
				result.Add(role);
			}

			return result;
		}
	}
}
