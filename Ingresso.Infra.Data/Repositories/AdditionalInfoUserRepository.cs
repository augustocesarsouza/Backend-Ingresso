using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class AdditionalInfoUserRepository : IAdditionalInfoUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AdditionalInfoUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdditionalInfoUser?> GetInfoUser(Guid idGuid)
        {
            var users = await
                _context.AdditionalInfoUsers
                .Where(x => x.UserId == idGuid)
                .Select(x => new AdditionalInfoUser(x.BirthDate, x.Gender, x.Phone, x.Cep, x.Logradouro, x.Numero, x.Complemento, x.Referencia, x.Bairro, x.Estado, x.Cidade))
                .FirstOrDefaultAsync();

            return users;
        }

        public async Task<AdditionalInfoUser?> GetByIdGuidUser(Guid idGuid)
        {
            var users = await
                _context.AdditionalInfoUsers
                .Where(x => x.UserId == idGuid)
                .FirstOrDefaultAsync();

            return users;
        }

        public async Task<AdditionalInfoUser> CreateInfo(AdditionalInfoUser infoUser)
        {
            await _context.AdditionalInfoUsers.AddAsync(infoUser);
            await _context.SaveChangesAsync();

            return infoUser;
        }

        public async Task<AdditionalInfoUser> UpdateAsync(AdditionalInfoUser infoUser)
        {
            _context.AdditionalInfoUsers.Update(infoUser);
            await _context.SaveChangesAsync();

            return infoUser;
        }
    }
}
