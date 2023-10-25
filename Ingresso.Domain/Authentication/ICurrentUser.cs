namespace Ingresso.Domain.Authentication
{
    public interface ICurrentUser
    {
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Password { get; set; }
        public bool IsValid { get; set; }
    }
}
