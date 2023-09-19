using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.Services.Abstractions
{
    public interface IAccountService
    {
        public void PostRegisterVM(RegisterVM model);
        public bool IsRightIdentifier(string email, string password);
    }
}
