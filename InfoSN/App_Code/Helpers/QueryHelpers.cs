using InfoSN.App_Code.Helpers.Entities.Abstractions;
using InfoSN.Models.Entities.Abstractions;
using System.Data;
using System.Reflection;

namespace InfoSN.App_Code.Helpers
{
	public static class QueryHelpers
	{
		public static string GenerateGetAllQuery(TableNames tableName)
		{
			return $"SELECT * FROM [{tableName}];";
		}

		public static string GenerateGetByQuery(TableNames tableName, string attributName, string attributValue)
		{
			return $"SELECT * FROM [{tableName}] WHERE {attributName} = '{attributValue}';";
		}

		public static string GenerateSecurePostQuery(Entity entity, TableNames nameTable)
		{
			string result = $"INSERT INTO [{nameTable}] VALUES (";
			PropertyInfo[] properties = entity.GetType().GetProperties();

			foreach (PropertyInfo property in properties)
			{
				result += $"@{property.Name}, ";
			}
			result = result[0..(result.Length - 2)];
			result += ");";

			return result.ToString();
		}

		public static void AddParametersToDbCommand(IDbCommand command, Entity entity)
		{
			PropertyInfo[] props = entity.GetType().GetProperties();

			foreach (PropertyInfo prop in props)
			{
				IDbDataParameter parameter = command.CreateParameter();

				parameter.ParameterName = $"@{prop.Name}";
				parameter.Value = prop.GetValue(entity) is not null ? prop.GetValue(entity) : DBNull.Value;

				command.Parameters.Add(parameter);
			}
		}

		public static T? GetEntity<T>(IDbConnection dbConnection, string query, EntityHelpers helpers) where T : Entity
		{
			T? result = null;

			dbConnection.Open();

			IDbCommand command = dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result = (T?)helpers.GenerateEntityFromDb(reader);
			}

			dbConnection.Close();

			return result;
		}

		public static IEnumerable<T> GetAllEntities<T>(IDbConnection dbConnection, string query, EntityHelpers helpers) where T : Entity
		{
			List<T> result = new List<T>();
			dbConnection.Open();

			IDbCommand command = dbConnection.CreateCommand();
			command.CommandText = query;
			IDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				T entity = (T)helpers.GenerateEntityFromDb(reader);
				result.Add(entity);
			}

			dbConnection.Close();

			return result;
		}
	}
}
