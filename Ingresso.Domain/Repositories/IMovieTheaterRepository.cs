using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IMovieTheaterRepository
    {
        public Task<MovieTheater?> GetById(Guid guidId);
        public Task<List<MovieTheater>?> GetByMovieId(Guid idMovie);
        public Task<MovieTheater> Delete(MovieTheater movieTheater);
    }
}
