using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserPermission?>?> GetAllPermissionUser(Guid? idUser)
        {
            var permission = await
                _context
                .UserPermissions
                .Include(x => x.Permission)
                .Where(x => x.UserId == idUser)
                .Select(x  => x.Permission != null ? new UserPermission(x.Id, x.UserId, new Permission(x.Permission.VisualName ?? "", x.Permission.PermissionName ?? ""))
                : null)
                .ToListAsync();

            return permission;
        }
    }
}
