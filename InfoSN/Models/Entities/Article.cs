using InfoSN.App_Code.Attributes;
using InfoSN.Models.Entities.Abstractions;

namespace InfoSN.Models.Entities
{
	public class Article : Entity
	{
		[SqlColumnName("id_article")]
		public string Id { get; set; } = null!;

		[SqlColumnName("title_article")]
		public string Title { get; set; } = null!;

		[SqlColumnName("content_article")]
		public string Description { get; set; } = null!;

		[SqlColumnName("post_date_article")]
		public DateTime PostDate { get; set; }

		[SqlColumnName("edit_date_article")]
		public DateTime? EditDate { get; set; }

		[SqlColumnName("id_user")]
		public string IdUser { get; set; } = null!;
	}
}
