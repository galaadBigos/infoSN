using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
	public interface IUserRepository
	{
		public void PostUser(User user);
		public User? GetUser(string email);
		//public User? GetUserAndRoles(string userId);
		public IEnumerable<User> GetAllUsers();
	}
}
