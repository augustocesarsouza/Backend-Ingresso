namespace Ingresso.Domain.Entities
{
    public class Region
    {
        public Guid Id { get; private set; }
        public string? State { get; private set; }
        public string? City { get; private set; }

        public Region()
        {
        }

        public Region(Guid id)
        {
            Id = id;
        }
    }
}
