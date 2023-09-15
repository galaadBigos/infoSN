using InfoSN.Models.Entities.Abstractions;
using System.Data;
using System.Reflection;

namespace InfoSN.App_Code.Helpers
{
    public static class CRUDHelper
    {
        public static string GenerateSecurePostQuery(Entity entity, TableNames nameTable)
        {
            string result = $"INSERT INTO User VALUES (";
            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Id")
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
                if (prop.Name != "Id")
                {
                    IDbDataParameter parameter = command.CreateParameter();

                    parameter.ParameterName = $"@{prop.Name}";
                    parameter.Value = prop.GetValue(entity);

                    command.Parameters.Add(parameter);
                }
            }
        }
    }
}
