namespace Ingresso.Domain.Entities
{
    public class CinemaMovie
    {
        public Guid? Id { get; private set; }

        public Guid? CinemaId { get; private set; }
        public Cinema? Cinema { get; private set; }
        public Guid? MovieId { get; private set; }
        public Movie? Movie { get; private set; }
        public Guid? RegionId { get; private set; }
        public Region? Region { get; private set; }

        public string? ScreeningSchedule { get; private set; }

        public CinemaMovie()
        {
        }

        public CinemaMovie(Cinema? cinema, string? screeningSchedule)
        {
            Cinema = cinema;
            ScreeningSchedule = screeningSchedule;
        }
    }
}
