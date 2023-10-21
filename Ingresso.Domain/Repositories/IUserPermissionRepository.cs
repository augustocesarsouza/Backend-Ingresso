using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IUserPermissionRepository
    {
        public Task<ICollection<UserPermission?>?> GetAllPermissionUser(int? idUser);
    }
}
