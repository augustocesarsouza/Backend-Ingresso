using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IMovieTheaterService
    {
        public Task<ResultService<MovieTheaterDTO>> GetByIdMovie(Guid idMovie);
        public Task<ResultService<MovieTheaterDTO>> Delete(Guid idMovie);
    }
}
