namespace Ingresso.Application.DTOs
{
    public class MovieTheaterDTO
    {
        public Guid Id { get; set; }

        public Guid MovieId { get;  set; }
        public MovieDTO? MovieDTO { get; set; }

        public Guid RegionId { get; set; }
        public RegionDTO? RegionDTO { get; set; }
    }
}
