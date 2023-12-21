namespace Ingresso.Application.DTOs
{
    public class RegionCinemaDTO
    {
        public Guid Id { get; set; }

        public Guid RegionId { get; set; }
        public RegionDTO? RegionDTO { get; set; }

        public Guid CinemaId { get; set; }
        public CinemaDTO? CinemaDTO { get; set; }
    }
}
