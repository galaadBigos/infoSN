using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;
using InfoSN.Services.Implementations;
using Moq;

namespace InfoSN.UnitTests.Services
{
    public class AccountServiceTests
    {
        private Fixture _fixture;

        private readonly IAccountService _accountService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAccountManager> _accountManagerMock;

        public AccountServiceTests()
        {
            _fixture = new Fixture();
            _userRepositoryMock = new Mock<IUserRepository>();
            _accountManagerMock = new Mock<IAccountManager>();
            _accountService = new AccountService(_accountManagerMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void PostUser_Should_Call_Once_UserRepository_PostUser_With_An_User()
        {
            _userRepositoryMock.Setup(m => m.PostUser(It.IsAny<User>()));
            RegisterVM model = _fixture.Create<RegisterVM>();

            _accountService.PostRegisterVM(model);

            _userRepositoryMock.Verify(m => m.PostUser(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void CreateUser_Should_Return_User_According_To_RegisterVM_Model()
        {
            RegisterVM model = _fixture.Create<RegisterVM>();

            User user = _accountService.CreateUser(model);

            user.Should().NotBeNull();
            user.Id.Should().NotBeNullOrEmpty();
            user.UserName.Should().Be(model.UserName);
            user.Email.Should().Be(model.Email);
            user.Password.Should().NotBe(model.Password);
            user.RegistrationDate.Should().BeSameDateAs(DateTime.Now);
        }
    }
}
