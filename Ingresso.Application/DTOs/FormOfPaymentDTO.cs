namespace Ingresso.Application.DTOs
{
    public class FormOfPaymentDTO
    {
        public Guid? Id { get; set; }
        public string? FormName { get; set; }
        public string? Price { get;  set; }
        public Guid? MovieId { get; set; }
        public MovieDTO? MovieDTO { get; set; }
    }
}
