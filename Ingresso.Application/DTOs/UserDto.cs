namespace Ingresso.Application.DTOs
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? PasswordHash { get; set; }
        public string? BirthDateString { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public int? ConfirmEmail { get; set; }

        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Cep { get; set; } = "";
        public string Logradouro { get; set; } = "";
        public string Numero { get; set; } = "";
        public string Complemento { get; set; } = "";
        public string Referencia { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Cidade { get; set; } = "";
    }
}
