using Ingresso.Application.Services.Interfaces;
using SecureIdentity.Password;

namespace Ingresso.Application.Services
{
    public class PasswordHasherWrapper : IPasswordHasherWrapper
    {
        public bool Verify(string hashedPassword, string providedPassword)
        {
            return PasswordHasher.Verify(hashedPassword ?? "", providedPassword);
        }
    }
}
