using InfoSN.App_Code.Helpers.Entities.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.Entities.Abstractions;
using System.Data;

namespace InfoSN.App_Code.Helpers.Entities
{
	public class UserHelpers : EntityHelpers
	{
		public static User GenerateUserFromDb(IDataReader reader)
		{
			return new User()
			{
				Id = reader.GetString(0),
				UserName = reader.GetString(1),
				Email = reader.GetString(2),
				Password = reader.GetString(3),
				SaltPassword = reader.GetString(4),
				RegistrationDate = reader.GetDateTime(5),
			};
		}

		public override Entity GenerateEntityFromDb(IDataReader reader)
		{
			return GenerateUserFromDb((IDataReader)reader);
		}
	}
}
