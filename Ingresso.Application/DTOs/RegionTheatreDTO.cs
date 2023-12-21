using Ingresso.Domain.Entities;

namespace Ingresso.Application.DTOs
{
    public class RegionTheatreDTO
    {
        public Guid Id { get; set; }

        public Guid TheatreId { get; set; }
        public TheatreDTO? TheatreDTO { get; set; }

        public Guid RegionId { get; set; }
        public RegionDTO? RegionDTO { get; set; }
    }
}
