using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
	public interface IRoleRepository
	{
		public IEnumerable<Role> GetUserRolesById(string userId);
		public Role? GetRoleByName(string roleName);
	}
}
