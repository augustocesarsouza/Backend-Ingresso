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

        public User(int id, string? name, string? passwordHash, string? email) : this(id, name)
        {
            PasswordHash = passwordHash;
            Email = email;
        }

        public void ValidatorToken(string token)
        {
            DomainValidationException.When(string.IsNullOrEmpty(token), "Token not generated");
            Token = token;
        }
    }
}

//public User(string name, string email, string phone, string cpf, DateTime birthDate)
//{
//    Validator(name, email, phone, cpf, birthDate);
//}

//public void Validator(string name, string email, string phone, string cpf, DateTime? birthDate)
//{
//    DomainValidationException.When(string.IsNullOrEmpty(name), "Name Not null or empety");
//    DomainValidationException.When(string.IsNullOrEmpty(email), "Email Not null or empety");
//    DomainValidationException.When(string.IsNullOrEmpty(phone), "Phone Not null or empety");
//    DomainValidationException.When(string.IsNullOrEmpty(cpf), "cpf Not null or empety");
//    DomainValidationException.When(!birthDate.HasValue, "birthDate Should be informed");

//    Name = name;
//    Email = email;
//    Phone = phone;
//    Cpf = cpf;
//    BirthDate = birthDate;
//}