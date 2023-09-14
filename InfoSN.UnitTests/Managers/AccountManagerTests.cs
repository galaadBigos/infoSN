using AutoFixture;
using FluentAssertions;
using InfoSN.Managers;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;

namespace InfoSN.UnitTests.Managers
{
    public class AccountManagerTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void CreateUser_Should_Return_User_According_To_RegisterVM_Model()
        {
            RegisterVM model = _fixture.Create<RegisterVM>();

            User user = AccountManager.CreateUser(model);

            user.Should().NotBeNull();
            user.Id.Should().NotBeNullOrEmpty();
            user.UserName.Should().Be(model.UserName);
            user.Email.Should().Be(model.Email);
            user.Password.Should().NotBe(model.Password);
            user.RegistrationDate.Should().NotBeAfter(DateTime.Now);
        }
    }
}
