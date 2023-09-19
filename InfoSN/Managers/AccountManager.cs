using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace InfoSN.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly PasswordHasherOptions _hasher;

        public AccountManager(IOptions<PasswordHasherOptions> hasher)
        {
            _hasher = hasher.Value;
        }

        public User CreateUser(RegisterVM model)
        {
            byte[] salt = CreateSaltPassword();

            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName!,
                Email = model.Email!,
                Password = GetHashPassword(model.Password!, salt),
                SaltPassword = Convert.ToBase64String(salt),
                RegistrationDate = DateTime.Now,
            };
        }

        public bool VerifyPassword(User user, string passwordToVerify)
        {
            string userPassword = GetHashPassword(user.Password, Encoding.UTF8.GetBytes(user.SaltPassword));

            return userPassword == passwordToVerify;
        }

        private byte[] CreateSaltPassword()
        {
            return RandomNumberGenerator.GetBytes(_hasher.KeySize);
        }

        private string GetHashPassword(string password, byte[] salt)
        {
            byte[] bytesPassword = Encoding.UTF8.GetBytes(password);
            int iterations = _hasher.Iterations;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            int keySize = _hasher.KeySize;

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(bytesPassword, salt, iterations, hashAlgorithm, keySize);

            return Convert.ToHexString(hash);
        }
    }
}
