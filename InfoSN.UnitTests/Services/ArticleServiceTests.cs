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

		[Fact]
		public void GetDetailsArtickeVM_Should_Return_Null_If_Any_Article_From_ArticleRepository()
		{
			_articleRepositoryMock.Setup(m => m.GetArticle(It.IsAny<string>())).Returns(() => null);

			DetailsArticleVM? result = _articleService.GetDetailsArticleVM(It.IsAny<string>());

			_articleRepositoryMock.Verify(m => m.GetArticle(It.IsAny<string>()), Times.Once);
			result.Should().BeNull();
		}

		[Fact]
		public void GetDetailsArtickeVM_Should_Return_Details_If_Any_Article_From_ArticleRepository()
		{
			Article article = _fixture.Create<Article>();
			_articleRepositoryMock.Setup(m => m.GetArticle(It.IsAny<string>())).Returns(article);

			DetailsArticleVM? result = _articleService.GetDetailsArticleVM(It.IsAny<string>());

			_articleRepositoryMock.Verify(m => m.GetArticle(It.IsAny<string>()), Times.Once);
			result.Should().NotBeNull();
			result.Should().BeOfType<DetailsArticleVM>();
		}

		[Fact]
		public void GetUpdateArtickeVM_Should_Return_Null_If_Any_Article_From_ArticleRepository()
		{
			_articleRepositoryMock.Setup(m => m.GetArticle(It.IsAny<string>())).Returns(() => null);

			UpdateArticleVM? result = _articleService.GetUpdateArticleVM(It.IsAny<string>());

			_articleRepositoryMock.Verify(m => m.GetArticle(It.IsAny<string>()), Times.Once);
			result.Should().BeNull();
		}

		[Fact]
		public void GetUpdateArtickeVM_Should_Return_Update_If_Any_Article_From_ArticleRepository()
		{
			Article article = _fixture.Create<Article>();
			_articleRepositoryMock.Setup(m => m.GetArticle(It.IsAny<string>())).Returns(article);

			UpdateArticleVM? result = _articleService.GetUpdateArticleVM(It.IsAny<string>());

			_articleRepositoryMock.Verify(m => m.GetArticle(It.IsAny<string>()), Times.Once);
			result.Should().NotBeNull();
			result.Should().BeOfType<UpdateArticleVM>();
		}

		[Fact]
		public void PostArticle_Should_Call_PostArticle_Method_ArticleRepository()
		{
			NewArticleVM model = _fixture.Create<NewArticleVM>();

			_articleService.PostArticle(model);

			_articleRepositoryMock.Verify(m => m.PostArticle(It.IsAny<Article>()), Times.Once);
		}

		[Fact]
		public void UpdateArticle_Should_Call_UpdateArticle_Method_ArticleRepository()
		{
			UpdateArticleVM model = _fixture.Create<UpdateArticleVM>();

			_articleService.UpdateArticle(model);

			_articleRepositoryMock.Verify(m => m.UpdateArticle(It.IsAny<Article>()), Times.Once);
		}
	}
}
