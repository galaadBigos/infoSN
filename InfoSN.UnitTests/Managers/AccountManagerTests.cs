using InfoSN.Managers.Abstractions;
using InfoSN.Managers.Implementations;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Options;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;
using InfoSN.Services.Implementations;
using Microsoft.Extensions.Options;
using Moq;

namespace InfoSN.UnitTests.Managers
{
	public class AccountManagerTests
	{
		private Fixture _fixture = new Fixture();
		private Mock<IOptions<PasswordHasherOptions>> _optionsMock;
		private IAccountService _accountService;
		private Mock<IUserRepository> _userRepositoryMock;
		private Mock<IRoleRepository> _roleRepositoryMock;
		private Mock<IUserRoleRepository> _userRoleRepositoryMock;
		private PasswordHasherOptions _options;
		private IAccountManager _accountManager;

		public AccountManagerTests()
		{
			_optionsMock = new Mock<IOptions<PasswordHasherOptions>>();
			_options = new PasswordHasherOptions
			{
				KeySize = 32,
				Iterations = 1000
			};
			_optionsMock.Setup(m => m.Value).Returns(_options);
			_userRepositoryMock = new Mock<IUserRepository>();
			_roleRepositoryMock = new Mock<IRoleRepository>();
			_userRoleRepositoryMock = new Mock<IUserRoleRepository>();

			_accountManager = new AccountManager(_optionsMock.Object, _userRepositoryMock.Object);
			_accountService = new AccountService(_accountManager, _userRepositoryMock.Object, _roleRepositoryMock.Object, _userRoleRepositoryMock.Object);
		}

		[Fact]
		public void GetHashPassword_Should_Return_The_Same_Strings_With_Same_Parameters()
		{
			string password = _fixture.Create<string>();
			string salt = _fixture.Create<string>();

			string result1 = _accountManager.GetHashPassword(password, salt);
			string result2 = _accountManager.GetHashPassword(password, salt);

			result1.Should().NotBeNull();
			result2.Should().NotBeNull();

			result1.Should().NotBeEmpty();
			result2.Should().NotBeEmpty();

			result1.Should().Be(result2);
		}

		[Fact]
		public void VerifyPassword_Should_Return_True_If_LoginPassword_Is_The_Same_Of_User_Password()
		{
			string password = _fixture.Create<string>();
			RegisterVM model = new RegisterVM()
			{
				UserName = _fixture.Create<string>(),
				Email = _fixture.Create<string>(),
				Password = password,
			};
			User user = _accountService.CreateUser(model);

			bool result = _accountManager.VerifyPassword(user, password);

			result.Should().BeTrue();
		}

		[Fact]
		public void VerifyPassword_Should_Return_False_If_LoginPassword_Is_Not_The_Same_Of_User_Password()
		{
			string password = _fixture.Create<string>();
			string wrongPassword = _fixture.Create<string>();
			RegisterVM model = new RegisterVM()
			{
				UserName = _fixture.Create<string>(),
				Email = _fixture.Create<string>(),
				Password = password,
			};
			User user = _accountService.CreateUser(model);

			bool result = _accountManager.VerifyPassword(user, wrongPassword);

			result.Should().BeFalse();
		}

		[Fact]
		public void IsRightIdentifier_Should_Return_False_If_User_Is_Null()
		{
			LoginVM model = _fixture.Create<LoginVM>();
			_userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
				.Returns(() => null);

			string email = _fixture.Create<string>();
			string password = _fixture.Create<string>();

			bool isRightIdentifier = _accountManager.IsRightIdentifier(model);

			isRightIdentifier.Should().BeFalse();
			_userRepositoryMock.Verify(m => m.GetUser(It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public void IsRightIdentifier_Should_Return_False_If_VerifyPassword_Return_False()
		{
			LoginVM model = _fixture.Create<LoginVM>();
			_userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
	.Returns(_fixture.Create<User>());

			string email = _fixture.Create<string>();
			string password = _fixture.Create<string>();

			bool result = _accountManager.IsRightIdentifier(model);

			result.Should().BeFalse();
		}

		[Fact]
		public void IsRightIdentifier_Should_Return_True_If_VerifyPassword_Return_True()
		{
			string email = _fixture.Create<string>();
			string password = _fixture.Create<string>();

			RegisterVM registerModel = new RegisterVM()
			{
				UserName = _fixture.Create<string>(),
				Email = email,
				Password = password,
			};

			User user = _accountService.CreateUser(registerModel);
			_userRepositoryMock.Setup(m => m.GetUser(It.IsAny<string>()))
	.Returns(user);

			LoginVM loginModel = new LoginVM()
			{
				Email = email,
				Password = password,
				IsPersistent = true,
			};

			bool isRightIdentifier = _accountManager.IsRightIdentifier(loginModel);

			isRightIdentifier.Should().BeTrue();
		}
	}
}
