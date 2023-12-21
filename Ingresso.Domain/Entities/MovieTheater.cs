namespace Ingresso.Domain.Entities
{
    public class MovieTheater
    {
        public Guid Id { get; private set; }

        public Guid MovieId { get; private set; }
        public Movie? Movie { get; private set; }

        public Guid RegionId { get; private set; }
        public Region? Region { get; private set; }

        public MovieTheater()
        {
        }

        public void AddMovieIdAndRegionId(Guid movieId, Guid regionId)
        {
            MovieId = movieId;
            RegionId = regionId;
        }
    }
}
