using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IMovieRepository
    {
        public Task<Movie?> GetById(Guid guid);
        public Task<Movie?> GetInfoMoviesById(Guid guid);
        public Task<Movie?> GetStatusMovie(string statusMovie);
        public Task<Movie?> GetByIdAllProps(Guid guid);
        public Task<List<Movie>> GetAllMovieByRegionId(Guid regionId);
        public Task<Movie?> Create(Movie movie);
        public Task<Movie> Delete(Movie movie);
        public Task<Movie> Update(Movie movie);
    }
}
