using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;

namespace InfoSN.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountManager _accountManager;
        private readonly IUserRepository _userRepository;

        public UserService(IAccountManager accountManager, IUserRepository userRepository)
        {
            _accountManager = accountManager;
            _userRepository = userRepository;
        }

        public void PostRegisterVM(RegisterVM model)
        {
            User user = _accountManager.CreateUser(model);
            _userRepository.PostUser(user);
        }
    }
}
