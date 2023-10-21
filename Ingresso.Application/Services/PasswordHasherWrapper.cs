using Ingresso.Application.Services.Interfaces;
using SecureIdentity.Password;

namespace Ingresso.Application.Services
{
    public class PasswordHasherWrapper : IPasswordHasherWrapper
    {
        public bool Verify(string hash, string password, short keySize = 32, int iterations = 10000, char splitChar = '.', string privateKey = "")
        {
            return PasswordHasher.Verify(hash ?? "", password, keySize, iterations, splitChar, privateKey);
        }

        public string Hash(string password, short saltSize = 16, short keySize = 32, int iterations = 10000, char splitChar = '.', string privateKey = "")
        {
            return PasswordHasher.Hash(password, saltSize, keySize, iterations, splitChar, privateKey);
        }
    }
}
