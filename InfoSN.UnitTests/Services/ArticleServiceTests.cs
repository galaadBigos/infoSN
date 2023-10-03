using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Articles;
using InfoSN.Repositories.Abstractions;
using InfoSN.Services.Abstractions;
using InfoSN.Services.Implementations;

namespace InfoSN.UnitTests.Services
{
	public class ArticleServiceTests
	{
		private Fixture _fixture;

		private readonly IArticleService _articleService;
		private readonly Mock<IArticleRepository> _articleRepositoryMock;
		private readonly Mock<IUserRepository> _userRepositoryMock;

		public ArticleServiceTests()
		{
			_fixture = new Fixture();

			_articleRepositoryMock = new Mock<IArticleRepository>();
			_userRepositoryMock = new Mock<IUserRepository>();

			_articleService = new ArticleService(_articleRepositoryMock.Object, _userRepositoryMock.Object);
		}

		[Fact]
		public void GetAllArticles_Should_Return_All_Articles_Of_Repository()
		{
			int articleNumber = 3;
			string userId = Guid.NewGuid().ToString();

			IEnumerable<Article> articles = _fixture.CreateMany<Article>(articleNumber);
			articles.ToList().ForEach(a => a.IdUser = userId);

			User user = _fixture.Create<User>();
			user.Id = userId;

			IEnumerable<User> users = new List<User>()
			{
				user,
			};

			_articleRepositoryMock.Setup(m => m.GetAllArticles()).Returns(articles);
			_userRepositoryMock.Setup(m => m.GetAllUsers()).Returns(users);

			IEnumerable<DisplayArticleVM> result = _articleService.GetAllArticles();

			result.Should().NotBeNull();
			result.Should().HaveCount(articleNumber);
		}

		[Fact]
		public void GetAllArticles_Should_Return_Empty_IEnumerable_If_Repository_Return_Any_User()
		{
			int articleNumber = 0;

			IEnumerable<Article> articles = _fixture.CreateMany<Article>(articleNumber);

			IEnumerable<User> users = _fixture.Create<IEnumerable<User>>();

			_articleRepositoryMock.Setup(m => m.GetAllArticles()).Returns(articles);
			_userRepositoryMock.Setup(m => m.GetAllUsers()).Returns(users);

			IEnumerable<DisplayArticleVM> result = _articleService.GetAllArticles();

			result.Should().NotBeNull();
			result.Should().BeEmpty();
		}
	}
}
