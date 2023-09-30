using InfoSN.App_Code.Attributes;
using InfoSN.App_Code.Helpers.Entities.Abstractions;
using InfoSN.Models.Entities.Abstractions;
using System.Data;
using System.Reflection;

namespace InfoSN.App_Code.Helpers
{
	public static class QueryHelpers
	{
		public static string GenerateGetAllQuery(TableName tableName)
		{
			return $"SELECT * FROM [{tableName}];";
		}

		public static string GenerateGetByQuery(TableName tableName, string attributeName, string attributeValue)
		{
			return $"SELECT * FROM [{tableName}] WHERE {attributeName} = '{attributeValue}';";
		}

		public static string GenerateSecurePostQuery(Entity entity, TableName nameTable)
		{
			PropertyInfo[] properties = entity.GetType().GetProperties();

			string result = $"INSERT INTO [{nameTable}] VALUES (";

			foreach (PropertyInfo property in properties)
			{
				result += $"@{property.Name}, ";
			}

			result = result[0..(result.Length - 2)];
			result += ");";

			return result;
		}

		public static string GenerateSecureUpdateQuery(Entity entity, TableName tableName)
		{
			PropertyInfo[] properties = entity.GetType().GetProperties();

			string result = $"UPDATE [{tableName}] SET ";

			foreach (PropertyInfo property in properties)
			{
				string? sqlColumnName = property.GetCustomAttribute<SqlColumnNameAttribute>()?.Name;
				result += $"{sqlColumnName} = @{property.Name}, ";
			}

			result = result[0..(result.Length - 2)];

			string? idColumnName = properties[0].GetCustomAttribute<SqlColumnNameAttribute>()?.Name;
			string? idColumnValue = (string?)properties[0].GetValue(entity);

			result += $" WHERE {idColumnName} = '{idColumnValue}';";

			return result;
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
			List<T> result = new();

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

		public static void PostEntity(IDbConnection dbConnection, string query, Entity entity)
		{
			dbConnection.Open();

			IDbCommand command = dbConnection.CreateCommand();
			command.CommandText = query;
			AddParametersToDbCommand(command, entity);

			if (command.ExecuteNonQuery() <= 0)
			{
				throw new Exception("The post query did not work");
			}

			dbConnection.Close();
		}

		public static void UpdateEntity(IDbConnection dbConnection, string query, Entity entity)
		{
			dbConnection.Open();
			IDbCommand command = dbConnection.CreateCommand();

			command.CommandText = query;

			AddParametersToDbCommand(command, entity);

			if (command.ExecuteNonQuery() <= 0)
			{
				throw new Exception("The update query did not work");
			}

			dbConnection.Close();
		}
	}
}
