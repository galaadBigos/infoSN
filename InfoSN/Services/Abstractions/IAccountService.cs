using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Services.Abstractions
{
    public interface IAccountService
    {
        public void PostRegisterVM(RegisterVM model);
        public User CreateUser(RegisterVM model);
    }
}
