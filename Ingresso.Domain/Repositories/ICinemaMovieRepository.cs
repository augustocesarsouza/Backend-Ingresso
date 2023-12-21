using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface ICinemaMovieRepository
    {
        public Task<List<CinemaMovie>> GetByRegionCinemaIdAndMovieId(Guid regionId, Guid movieId);
        public Task<CinemaMovie> Create(CinemaMovie cinemaMovie);
    }
}
