using InfoSN.Models.Entities;

namespace InfoSN.Repositories.Abstractions
{
    public interface IUserRepository
    {
        public void PostUser(User user);
    }
}
