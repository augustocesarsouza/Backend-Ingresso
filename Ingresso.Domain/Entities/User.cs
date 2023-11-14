namespace Ingresso.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Cpf { get; private set; }
        public string? PasswordHash { get; private set; }
        public int? ConfirmEmail { get; private set; }
        //public string? Phone { get; private set; }
        //public DateTime? BirthDate { get; private set; }

        public string? Token { get; private set; }

        public User()
        {
        }

        public User(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }

        public User(Guid id, string? name, string? email) : this(id, name)
        {
            Email = email;
        }

        public User(Guid id, string? email, string? cpf, string? passwordHash)
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

        public void ConfirmedEmail(int value)
        {
            ConfirmEmail = value;
        }
    }
}