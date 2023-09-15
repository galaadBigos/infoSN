using AutoFixture;
using InfoSN.Managers;
using InfoSN.Managers.Abstractions;
using InfoSN.Options;
using Microsoft.Extensions.Options;
using Moq;

namespace InfoSN.UnitTests.Managers
{
    public class AccountManagerTests
    {
        private Fixture _fixture = new Fixture();
        private Mock<IOptions<PasswordHasherOptions>> _options = new();
        private IAccountManager _accountManager;

        public AccountManagerTests()
        {
            _accountManager = new AccountManager(_options.Object);
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
