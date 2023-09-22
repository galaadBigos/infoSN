using InfoSN.Managers.Abstractions;
using InfoSN.Managers.Implementations;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Options;
using InfoSN.Repositories.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace InfoSN.UnitTests.Managers
{
    public class AccountManagerTests
    {
        private Fixture _fixture = new Fixture();
        private Mock<IOptions<PasswordHasherOptions>> _optionsMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private PasswordHasherOptions _options;
        private IAccountManager _accountManager;

        public AccountManagerTests()
        {
            _optionsMock = new Mock<IOptions<PasswordHasherOptions>>();
            _options = new PasswordHasherOptions
            {
                KeySize = 64,
                Iterations = 10000
            };
            _userRepositoryMock = new Mock<IUserRepository>();

            _optionsMock.Setup(m => m.Value).Returns(_options);

            _accountManager = new AccountManager(_optionsMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void CreateUser_Should_Return_User_According_To_RegisterVM_Model()
        {
            RegisterVM model = _fixture.Create<RegisterVM>();

            User user = _accountManager.CreateUser(model);

            user.Should().NotBeNull();
            user.Id.Should().NotBeNullOrEmpty();
            user.UserName.Should().Be(model.UserName);
            user.Email.Should().Be(model.Email);
            user.Password.Should().NotBe(model.Password);
            user.RegistrationDate.Should().BeSameDateAs(DateTime.Now);
        }
    }
}
