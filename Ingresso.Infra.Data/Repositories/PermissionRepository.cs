using Ingresso.Infra.Data.Context;

namespace Ingresso.Infra.Data.Repositories
{
    public class PermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
