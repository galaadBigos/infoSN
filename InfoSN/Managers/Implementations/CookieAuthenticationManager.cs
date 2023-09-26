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

		public CookieAuthenticationManager(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public List<Claim> CreateLoginClaims(LoginVM model)
		{
			User? user = _userRepository.GetUser(model.Email!);

			return new List<Claim>()
			{
				new Claim(ClaimTypes.Name, model.Email!),
				new Claim(ClaimTypes.Role, "User"),
				new Claim("UserId", user!.Id)
			};
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
