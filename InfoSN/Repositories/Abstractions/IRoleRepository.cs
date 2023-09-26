using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
	public interface IRoleRepository
	{
		public IEnumerable<Role> GetRoles(string userId);
	}
}
