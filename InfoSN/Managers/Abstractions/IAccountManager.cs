using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Managers.Abstractions
{
    public interface IAccountManager
    {
        public User CreateUser(RegisterVM model);
        public bool VerifyPassword(User user, string passwordToVerify);
    }
}
