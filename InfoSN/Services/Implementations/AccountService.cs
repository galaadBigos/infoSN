using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;

namespace InfoSN.Services.Implementations
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
            User user = CreateUser(model);
            _userRepository.PostUser(user);
        }

        public User CreateUser(RegisterVM model)
        {
            string salt = Guid.NewGuid().ToString();

            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName!,
                Email = model.Email!,
                Password = _accountManager.GetHashPassword(model.Password!, salt),
                SaltPassword = salt,
                RegistrationDate = DateTime.Now,
            };
        }
    }
}
