using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services;
using InfoSN.Services.Abstractions;
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
        public void IsRightIdentifier_Should_Return_False_If_Repository_Return_Null()
        {
            _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns(() => null);

            string email = _fixture.Create<string>();
            string password = _fixture.Create<string>();

            bool isRightIdentifier = _accountService.IsRightIdentifier(email, password);

            isRightIdentifier.Should().BeFalse();
            _userRepositoryMock.Verify(m => m.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void IsRightIdentifier_Should_Return_False_If_VerifyPassword_Return_False()
        {
            _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
    .Returns(new User());
            _accountManagerMock.Setup(m => m.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(() => false);

            string email = _fixture.Create<string>();
            string password = _fixture.Create<string>();

            bool isRightIdentifier = _accountService.IsRightIdentifier(email, password);

            isRightIdentifier.Should().BeFalse();
            _accountManagerMock.Verify(m => m.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void IsRightIdentifier_Should_Return_True_If_VerifyPassword_Return_True()
        {
            _userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
    .Returns(new User());
            _accountManagerMock.Setup(m => m.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(() => true);

            string email = _fixture.Create<string>();
            string password = _fixture.Create<string>();

            bool isRightIdentifier = _accountService.IsRightIdentifier(email, password);

            isRightIdentifier.Should().BeTrue();
            _accountManagerMock.Verify(m => m.VerifyPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
