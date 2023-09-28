using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
	public interface IUserRoleRepository
	{
		public void PostUserRole(UserRole userRole);
	}
}
