namespace Ingresso.Domain.Entities
{
    public class RegionTheatre
    {
        public Guid Id { get; private set; }

        public Guid TheatreId { get; private set; }
        public Theatre? Theatre { get; private set; }

        public Guid RegionId { get; private set; }
        public Region? Region { get; private set; }

        public RegionTheatre()
        {
        }

        public void SetTheatreAndRegionId(Guid theatreId, Guid regionId)
        {
            TheatreId = theatreId;
            RegionId = regionId;
        }
    }
}
