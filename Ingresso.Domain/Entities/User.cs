namespace Ingresso.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? EmailRecovery { get; private set; }
        public string? Phone { get; private set; }
        public string? Cpf { get; private set; }
        public string? PasswordHash { get; private set; }
        public DateTime? BirthDate { get; private set; }

        public string? Token { get; private set; }

        public User()
        {
        }

        public User(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public User(int id, string? email, string? cpf, string? passwordHash)
        {
            Id = id;
            Email = email;
            Cpf = cpf;
            PasswordHash = passwordHash;
        }

        public void ValidatorToken(string token)
        {
            DomainValidationException.When(string.IsNullOrEmpty(token), "Token not generated");
            Token = token;
        }
    }
}