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
	}
}
