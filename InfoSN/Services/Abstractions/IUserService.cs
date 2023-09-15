using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Services.Abstractions
{
    public interface IUserService
    {
        public void PostRegisterVM(RegisterVM model);
    }
}
