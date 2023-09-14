using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Managers
{
    public class AccountManager
    {
        public static User CreateUser(RegisterVM model)
        {
            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName!,
                Email = model.Email!,
                Password = model.Password!,
                RegistrationDate = DateTime.Now,
            };
        }
    }
}
