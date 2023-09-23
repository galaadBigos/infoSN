using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
    public interface IUserRepository
    {
        public void PostUser(User user);
        public User? GetUser(string email);
        public IEnumerable<User> GetAllUsers();
    }
}
