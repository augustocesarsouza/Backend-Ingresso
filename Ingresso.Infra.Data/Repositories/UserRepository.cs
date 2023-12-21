using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Ingresso.Infra.Data.Persistense;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Ingresso.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly IDistributedCache _distributedCache;
        private readonly ICacheRedisUti _cache;

        public UserRepository(ApplicationDbContext applicationDbContext, ICacheRedisUti cacheRedisUti)
        {
            _context = applicationDbContext;
            _cache = cacheRedisUti;
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserEmail(string email)
        {
            var user = await 
                _context
                .Users
                .Where(x => x.Email == email)
                .Select(x => new User(x.Id, x.Name))
                .FirstOrDefaultAsync();

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
            var cached = await _cache.GetStringAsyncWrapper(chaveKey);

            if (string.IsNullOrEmpty(cached))
            {
                var users = await _context
                    .Users
                    .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash, x.Name))
                    .ToListAsync();

                var cacheEntryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                };

                await _cache.SetStringAsyncWrapper(chaveKey, JsonConvert.SerializeObject(users), cacheEntryOptions);

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

        public async Task<User?> GetUserById(Guid id)
        {
            var user = await _context
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            var user = await _context
                .Users
                .Where(u => u.Email == email)
                .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash, x.Name))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByEmailOnlyPasswordHash(Guid idGuid)
        {

            var user = await _context
                .Users
                .Where(u => u.Id == idGuid)
                .Select(x => new User(x.PasswordHash))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByCpf(string cpf)
        {
            var user = await _context
                .Users
                .Where(u => u.Cpf == cpf)
                .Select(x => new User(x.Id, x.Email, x.Cpf, x.PasswordHash, x.Name))
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

        public async Task<User?> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
