using InfoSN.Data.Constants;
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
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRoleRepository _userRoleRepository;

		public AccountService(IAccountManager accountManager, IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
		{
			_accountManager = accountManager;
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_userRoleRepository = userRoleRepository;
		}

		public void PostRegisterVM(RegisterVM model)
		{
			User user = CreateUser(model);
			Role? role = _roleRepository.GetRoleByName(RoleName.User);

			if (role is not null)
			{
				UserRole userRole = new UserRole()
				{
					IdRole = role.Id,
					IdUser = user.Id,
				};

				_userRepository.PostUser(user);
				_userRoleRepository.PostUserRole(userRole);
			}
			else
			{
				throw new Exception();
			}
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
