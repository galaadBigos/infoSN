using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Managers.Abstractions
{
    public interface IAccountManager
    {
        public bool VerifyPassword(User user, string passwordToVerify);
        public string GetHashPassword(string password, string salt);
        public bool IsRightIdentifier(LoginVM model);


    }
}
