using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<ResultService<List<UserDto>>> GetUsers();
        public Task<ResultService<UserDto>> CheckEmailAlreadyExists(string email);
        public Task<ResultService<UserDto>> CreateAsync(UserDto? userDto);
    }
}
