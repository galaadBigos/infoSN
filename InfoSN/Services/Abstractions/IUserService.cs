using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Services.Abstractions
{
    public interface IUserService
    {
        public void PostUser(RegisterVM model);
    }
}
