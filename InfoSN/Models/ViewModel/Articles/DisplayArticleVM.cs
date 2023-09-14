using InfoSN.Models.Entities;

namespace InfoSN.Models.ViewModel.Articles
{
    public class DisplayArticleVM
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostDate { get; set; }
        public DateTime? EditDate { get; set; }
        public User User { get; set; } = null!;
    }
}
