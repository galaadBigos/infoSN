using InfoSN.Models.ViewModel.Articles;

namespace InfoSN.Services.Abstractions
{
    public interface IArticleService
    {
        public IEnumerable<DisplayArticleVM> GetAllArticles();
    }
}
