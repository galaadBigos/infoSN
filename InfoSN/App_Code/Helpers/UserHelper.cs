using InfoSN.Models.Entities;
using System.Data;

namespace InfoSN.App_Code.Helpers
{
    public static class UserHelper
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
    }
}
