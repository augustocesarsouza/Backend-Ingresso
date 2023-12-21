using Ingresso.Domain.Entities;

namespace Ingresso.Application.DTOs
{
    public class CinemaMovieDTO
    {
        public Guid? Id { get; set; }

        public Guid? CinemaId { get; set; }
        public CinemaDTO? CinemaDTO { get; set; }
        public Guid? MovieId { get; set; }
        public MovieDTO? MovieDTO { get; set; }
        public Guid? RegionId { get; set; }
        public RegionDTO? RegionDTO { get; set; }
        public string? ScreeningSchedule { get; set; }


    }
}
