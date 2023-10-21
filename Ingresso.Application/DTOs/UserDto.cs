namespace Ingresso.Application.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? EmailRecovery { get; set; }
        public string? Phone { get; set; }
        public string? Cpf { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDateString { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
    }
}
