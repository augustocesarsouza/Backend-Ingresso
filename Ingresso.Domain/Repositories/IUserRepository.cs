using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IUserRepository 
    {
        public Task<User?> CreateAsync(User user);
        public Task<User?> GetUserEmail(string email);
        public Task<List<User>?> GetUsers();
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> GetUserById(Guid id);
        public Task<User?> GetUserByCpf(string cpf);
        public Task<User?> CheckUserExits(string email, string cpf);
        public Task<User?> UpdateUser(User user);
    }
}
