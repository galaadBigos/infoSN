namespace InfoSN.Models.ViewModel.Articles
{
	public class DetailsArticleVM
	{
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateTime PostDate { get; set; }
		public DateTime? EditDate { get; set; }
		public string IdUser { get; set; } = null!;
	}
}
