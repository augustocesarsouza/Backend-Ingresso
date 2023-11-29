namespace Ingresso.Domain.Entities
{
    public class AdditionalInfoUser
    {
        public int Id { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string? Gender { get; private set; }
        public string? Phone { get; private set; }
        public string? Cep { get; private set; }
        public string? Logradouro { get; private set; }
        public string? Numero { get; private set; }
        public string? Complemento { get; private set; }
        public string? Referencia { get; private set; }
        public string? Bairro { get; private set; }
        public string? Estado { get; private set; }
        public string? Cidade { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public AdditionalInfoUser(DateTime? birthDate, string? gender, string? phone, string? cep, string? logradouro, string? numero, string? complemento, string? referencia, string? bairro, string? estado, string? cidade)
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
        }

        public void AddData(
            DateTime? birthDate, string? gender, string? phone, string? cep,
            string? logradouro, string? numero, string? complemento, string? referencia, 
            string? bairro, string? estado, string? cidade, Guid userId)
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
            UserId = userId;
        }
    }
}
