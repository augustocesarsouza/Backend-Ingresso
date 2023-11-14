using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserPermissionService
    {
        public Task<ResultService<List<UserPermissionDTO>>> GetAllPermissionUser(Guid? idUser);
    }
}
