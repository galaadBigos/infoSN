using InfoSN.Models.Entities.Abstractions;

namespace InfoSN.Models.Entities
{
	public class UserRole : Entity
	{
		public string IdUser { get; set; } = null!;
		public string IdRole { get; set; } = null!;
	}
}
