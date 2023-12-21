namespace Ingresso.Domain.Entities
{
    public class Cinema
    {
        public Guid Id { get; private set; }
        public string? NameCinema { get; private set; }
        public string? District { get; private set; }
        public string? Ranking { get; private set; }

        public Cinema()
        {
        }

        public Cinema(string? nameCinema, string? district, string? ranking)
        {
            NameCinema = nameCinema;
            District = district;
            Ranking = ranking;
        }
    }
}
