using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Ingresso.Infra.Data.Persistense;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Ingresso.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _distributedCache;

        public UserRepository(ApplicationDbContext applicationDbContext, IDistributedCache distributedCache)
        {
            _context = applicationDbContext;
            _distributedCache = distributedCache;
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>?> GetUsers()
        {
            //var users = await _context
            //    .Users
            //    .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash))
            //    .ToListAsync();

            //return users;

            var chaveKey = "UserList";
            var cached = await _distributedCache.GetStringAsync(chaveKey);

            if (string.IsNullOrEmpty(cached))
            {
                var users = await _context
                    .Users
                    .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash))
                    .ToListAsync();

                var cacheEntryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                };

                await _distributedCache.SetStringAsync(chaveKey, JsonConvert.SerializeObject(users), cacheEntryOptions);

                return users;
            }
            else
            {
                var userDto = JsonConvert.DeserializeObject<List<User>>(cached,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new PrivateResolver()
                    });

                if (userDto == null)
                    return null;

                return userDto;
            }
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
