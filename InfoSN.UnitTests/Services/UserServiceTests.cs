using AutoFixture;
using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services;
using InfoSN.Services.Abstractions;
using Moq;

namespace InfoSN.UnitTests.Services
{
    public class UserServiceTests
    {
        private Fixture _fixture;

        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAccountManager> _accountManagerMock;

        public UserServiceTests()
        {
            _fixture = new Fixture();
            _userRepositoryMock = new Mock<IUserRepository>();
            _accountManagerMock = new Mock<IAccountManager>();
            _userService = new UserService(_accountManagerMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void PostUser_Should_Call_Once_UserRepository_PostUser_With_An_User()
        {
            _userRepositoryMock.Setup(m => m.PostUser(It.IsAny<User>()));
            RegisterVM model = _fixture.Create<RegisterVM>();

            _userService.PostRegisterVM(model);

            _userRepositoryMock.Verify(m => m.PostUser(It.IsAny<User>()), Times.Once());
        }
    }
}
