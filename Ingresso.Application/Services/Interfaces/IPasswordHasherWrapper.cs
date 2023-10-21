namespace Ingresso.Application.Services.Interfaces
{
    public interface IPasswordHasherWrapper
    {
        bool Verify(string hashedPassword, string providedPassword);
    }
}
