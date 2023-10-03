using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;
using InfoSN.Services.Implementations;

namespace InfoSN.UnitTests.Services
{
	public class AccountServiceTests
	{
		private Fixture _fixture;

		private readonly IAccountService _accountService;
		private readonly Mock<IUserRepository> _userRepositoryMock;
		private readonly Mock<IAccountManager> _accountManagerMock;
		private readonly Mock<IRoleRepository> _roleRepositoryMock;
		private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock;

		public AccountServiceTests()
		{
			_fixture = new Fixture();

			_userRepositoryMock = new Mock<IUserRepository>();
			_roleRepositoryMock = new Mock<IRoleRepository>();
			_userRoleRepositoryMock = new Mock<IUserRoleRepository>();
			_accountManagerMock = new Mock<IAccountManager>();

			_accountService = new AccountService(_accountManagerMock.Object, _userRepositoryMock.Object, _roleRepositoryMock.Object, _userRoleRepositoryMock.Object);
		}

		[Fact]
		public void PostRegisterVM_Should_Call_Once_UserRepository_PostUser_And_UserRoleRepository_PostUserRole()
		{
			_userRepositoryMock.Setup(m => m.PostUser(It.IsAny<User>()));
			Role role = _fixture.Create<Role>();
			_roleRepositoryMock.Setup(m => m.GetRoleByName(It.IsAny<string>()))
				.Returns(role);
			RegisterVM model = _fixture.Create<RegisterVM>();
			_accountService.PostRegisterVM(model);

			_userRepositoryMock.Verify(m => m.PostUser(It.IsAny<User>()), Times.Once());
			_userRoleRepositoryMock.Verify(m => m.PostUserRole(It.IsAny<UserRole>()), Times.Once());
		}

		[Fact]
		public void PostRegisterVM_Should_Throw_An_Exception_If_Role_Is_Null()
		{
			_roleRepositoryMock.Setup(m => m.GetRoleByName(It.IsAny<string>()))
				.Returns(() => null);
			RegisterVM model = _fixture.Create<RegisterVM>();

			Action action = () => _accountService.PostRegisterVM(model);

			action.Should().Throw<Exception>();
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
