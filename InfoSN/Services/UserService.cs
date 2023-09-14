using InfoSN.Managers;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;

namespace InfoSN.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void PostUser(RegisterVM model)
        {
            User user = AccountManager.CreateUser(model);
            _userRepository.PostUser(user);
        }
    }
}
