using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace InfoSN.Managers.Implementations
{
	public class CookieAuthenticationManager : ICookieAuthenticationManager
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;

		public CookieAuthenticationManager(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}
		public List<Claim> CreateLoginClaims(LoginVM model)
		{
			User? user = _userRepository.GetUser(model.Email!);
			List<Role> roles = _roleRepository.GetRoles(user?.Id!).ToList();

			List<Claim> result = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, model.Email!),
				new Claim("UserId", user!.Id)
			};

			roles.ForEach(r => result.Add(new Claim(ClaimTypes.Role, r.Name)));

			return result;
		}

		public ClaimsIdentity CreateLoginIdentity(List<Claim> claims)
		{
			return new ClaimsIdentity(claims, "LoginCookie");
		}

		public AuthenticationProperties CreateLoginAuthenticationProperties(bool isPersistent)
		{
			return new AuthenticationProperties()
			{
				IsPersistent = isPersistent,
			};
		}
	}
}
