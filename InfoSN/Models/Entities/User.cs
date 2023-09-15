using InfoSN.Models.Entities.Abstractions;

namespace InfoSN.Models.Entities
{
    public class User : Entity
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SaltPassword { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
    }
}
