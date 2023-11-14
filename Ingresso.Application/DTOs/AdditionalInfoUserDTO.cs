using Ingresso.Domain.Entities;

namespace Ingresso.Application.DTOs
{
    public class AdditionalInfoUserDTO
    {
        public int Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public string? Bairro { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }

        public Guid UserId { get; set; }
        public UserDto? UserDto { get; set; }

        public AdditionalInfoUserDTO()
        {
        }

        public AdditionalInfoUserDTO(
            DateTime? birthDate, string? gender, string? phone, string? cep, string? logradouro, 
            string? numero, string? complemento, string? referencia, string? bairro, string? estado, string? cidade, Guid idUser)
        {
            BirthDate = birthDate;
            Gender = gender;
            Phone = phone;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Referencia = referencia;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            UserId = idUser;
        }
    }
}
