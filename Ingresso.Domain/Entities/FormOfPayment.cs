namespace Ingresso.Domain.Entities
{
    public class FormOfPayment
    {
        public Guid Id { get; private set; }
        public string? FormName { get; private set; }
        public string? Price { get; private set; }
        public Guid MovieId { get; private set; }
        public Movie? Movie { get; private set; }

        public FormOfPayment()
        {
        }

        public FormOfPayment(string? formName, string? price)
        {
            FormName = formName;
            Price = price;
        }
    }
}
