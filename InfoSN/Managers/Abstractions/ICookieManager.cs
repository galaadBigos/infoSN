using InfoSN.Models.ViewModel.Accounts;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace InfoSN.Managers.Abstractions
{
    public interface ICookieManager
    {
        public List<Claim> CreateLoginClaims(LoginVM model);

        public ClaimsIdentity CreateLoginIdentity(List<Claim> claims);

        public AuthenticationProperties CreateLoginAuthenticationProperties(bool isPersistent);
    }
}
