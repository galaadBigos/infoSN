using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;

namespace InfoSN.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountManager _accountManager;
        private readonly IUserRepository _userRepository;

        public AccountService(IAccountManager accountManager, IUserRepository userRepository)
        {
            _accountManager = accountManager;
            _userRepository = userRepository;
        }

        public void PostRegisterVM(RegisterVM model)
        {
            User user = _accountManager.CreateUser(model);
            _userRepository.PostUser(user);
        }

        public bool IsRightIdentifier(string email, string password)
        {
            User? user = _userRepository.GetUser(email);

            if (user == null)
                return false;

            else if (_accountManager.VerifyPassword(user, password))
                return true;

            else
                return false;
        }
    }
}
