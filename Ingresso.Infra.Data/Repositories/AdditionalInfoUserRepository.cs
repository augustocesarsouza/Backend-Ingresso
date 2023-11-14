using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;

namespace Ingresso.Infra.Data.Repositories
{
    public class AdditionalInfoUserRepository : IAdditionalInfoUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AdditionalInfoUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdditionalInfoUser> CreateInfo(AdditionalInfoUser infoUser)
        {
            await _context.AdditionalInfoUsers.AddAsync(infoUser);
            await _context.SaveChangesAsync();

            return infoUser;
        }
    }
}
