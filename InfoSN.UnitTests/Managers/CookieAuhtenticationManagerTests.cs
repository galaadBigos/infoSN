using InfoSN.Managers.Abstractions;
using InfoSN.Managers.Implementations;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Repositories.Abstractions;
using System.Security.Claims;

namespace InfoSN.UnitTests.Managers
{
	public class CookieAuhtenticationManagerTests
	{
		private readonly ICookieAuthenticationManager _cookieAuthenticationManager;

		private readonly Fixture _fixture;

		private readonly Mock<IUserRepository> _userRepository;
		private readonly Mock<IRoleRepository> _roleRepository;

		public CookieAuhtenticationManagerTests()
		{
			_fixture = new Fixture();

			_userRepository = new Mock<IUserRepository>();
			_roleRepository = new Mock<IRoleRepository>();

			_cookieAuthenticationManager = new CookieAuthenticationManager(_userRepository.Object, _roleRepository.Object);
		}

		[Fact]
		public void CreateLoginClaims_Should_Return_Claims_List_With_Name_And_UserId()
		{
			LoginVM loginVM = _fixture.Create<LoginVM>();

			string userId = _fixture.Create<string>();
			User user = _fixture.Create<User>();
			user.Id = userId;
			List<Role> roles = _fixture.Create<List<Role>>();

			_userRepository.Setup(m => m.GetUser(loginVM.Email!)).Returns(user);
			_roleRepository.Setup(m => m.GetUserRolesById(userId)).Returns(roles);

			List<Claim> result = _cookieAuthenticationManager.CreateLoginClaims(loginVM);

			result.Should().NotBeEmpty();
			result.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Should().BeOfType<Claim>().Which.Value.Should().Be(loginVM.Email);
			result.Where(c => c.Type == "UserId").FirstOrDefault().Should().BeOfType<Claim>().Which.Value.Should().Be(userId);
		}
	}
}
