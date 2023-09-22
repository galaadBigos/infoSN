using InfoSN.Managers.Abstractions;
using InfoSN.Models.ViewModel.Accounts;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace InfoSN.Managers.Implementations
{
    public class CookieAuthenticationManager : ICookieAuthenticationManager
    {
        public List<Claim> CreateLoginClaims(LoginVM model)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Email!),
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
