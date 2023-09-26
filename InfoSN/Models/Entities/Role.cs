using InfoSN.Models.Entities.Abstractions;

namespace InfoSN.Models.Entities
{
	public class Role : Entity
	{
		public string Id { get; set; } = null!;
		public string Name { get; set; } = null!;
	}
}
