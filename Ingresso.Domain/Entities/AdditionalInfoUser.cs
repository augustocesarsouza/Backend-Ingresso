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
       
    }
}
