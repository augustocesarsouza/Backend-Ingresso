using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResultService<UserDto>> Login(string cpfOrEmail, string password);
    }
}
