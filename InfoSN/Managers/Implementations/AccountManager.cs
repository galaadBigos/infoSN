using InfoSN.Managers.Abstractions;
using InfoSN.Models.Entities;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Options;
using InfoSN.Repositories.Abstractions;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace InfoSN.Managers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly PasswordHasherOptions _hasher;
        private readonly IUserRepository _userRepository;

        public AccountManager(IOptions<PasswordHasherOptions> hasher, IUserRepository userRepository)
        {
            _hasher = hasher.Value;
            _userRepository = userRepository;

        }

        public bool VerifyPassword(User user, string passwordToVerify)
        {
            string userLoginPassword = GetHashPassword(passwordToVerify, user.SaltPassword);

            return userLoginPassword == user.Password;
        }

        public string GetHashPassword(string password, string salt)
        {
            byte[] bytesPassword = Encoding.UTF8.GetBytes(password);
            byte[] bytesSalt = Encoding.UTF8.GetBytes(salt);
            int iterations = _hasher.Iterations;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            int keySize = _hasher.KeySize;

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(bytesPassword, bytesSalt, iterations, hashAlgorithm, keySize);

            return Convert.ToHexString(hash);
        }

        public bool IsRightIdentifier(LoginVM model)
        {
            string email = model.Email!;
            string password = model.Password!;
            User? user = _userRepository.GetUser(email);

            if (user == null)
                return false;

            else if (VerifyPassword(user, password))
                return true;

            else
                return false;
        }
    }
}
