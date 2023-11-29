using Ingresso.Application.DTOs;
using Ingresso.Domain.Entities;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResultService<UserDto>> Login(string cpfOrEmail, string password);
        public ResultService<string> Verfic(int code, string guidId);
        public ResultService<string> ResendCode(UserDto user);
    }
}
