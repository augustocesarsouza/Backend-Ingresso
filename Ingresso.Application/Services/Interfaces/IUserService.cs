using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResultService<UserDto>> CreateAsync(UserDto? userDto);
        public Task<ResultService<UserDto>> Login(string cpfOrEmail, string password);
    }
}
