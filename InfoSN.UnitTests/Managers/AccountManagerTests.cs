using InfoSN.Managers;
using InfoSN.Managers.Abstractions;
using InfoSN.Options;
using Microsoft.Extensions.Options;

namespace InfoSN.UnitTests.Managers
{
    public class AccountManagerTests
    {
        private Fixture _fixture = new Fixture();
        private IOptions<PasswordHasherOptions> _options;
        private IAccountManager _accountManager;

        public AccountManagerTests(IOptions<PasswordHasherOptions> options)
        {
            _options = options;
            _accountManager = new AccountManager(_options);
        }

        //[Fact]
        //public void CreateUser_Should_Return_User_According_To_RegisterVM_Model()
        //{


        //    RegisterVM model = _fixture.Create<RegisterVM>();

        //    User user = _accountManager.CreateUser(model);

        //    user.Should().NotBeNull();
        //    user.Id.Should().NotBeNullOrEmpty();
        //    user.UserName.Should().Be(model.UserName);
        //    user.Email.Should().Be(model.Email);
        //    user.Password.Should().NotBe(model.Password);
        //    user.RegistrationDate.Should().NotBeAfter(DateTime.Now);
        //}
    }
}
