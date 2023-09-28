using InfoSN.App_Code.Helpers;
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

		public Role? GetRoleByName(string roleName)
		{
			Role? result = null;
			string query = QueryHelpers.GenerateGetByQuery(_table, "label_role", roleName);

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result = RoleHelpers.GenerateRoleFromDb(reader);
			}

			return result;
		}

		public IEnumerable<Role> GetUserRolesById(string userId)
		{
			List<Role> result = new List<Role>();
			string query =
				$"SELECT * FROM [{_table}] " +
				$"INNER JOIN [UserRole] ON [{_table}].id_role = [UserRole].id_role " +
				$"WHERE [UserRole].id_user = '{userId}'";

			_dbConnection.Open();

			IDbCommand command = _dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Role role = RoleHelpers.GenerateRoleFromDb(reader);
				result.Add(role);
			}

			return result;
		}


	}
}
