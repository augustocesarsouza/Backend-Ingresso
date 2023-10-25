using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            var user = await _context
                .Users
                .Where(u => u.Email == email)
                .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByCpf(string cpf)
        {
            var user = await _context
                .Users
                .Where(u => u.Cpf == cpf)
                .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> CheckUserExits(string email, string cpf)
        {
            var user = await _context
                .Users
                .Where(u => u.Email == email || u.Cpf == cpf)
                .Select(x => new User(x.Id, x.Name))
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
