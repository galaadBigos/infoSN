using InfoSN.Models.Entities;
using System.Data;

namespace InfoSN.App_Code.Helpers.Entities
{
	public static class RoleHelpers
	{
		public static Role GenerateRoleFromDb(IDataReader reader)
		{
			return new Role()
			{
				Id = reader.GetString(0),
				Name = reader.GetString(1),
			};
		}
	}
}
