using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<ResultService<List<MovieDTO>>> GetAllMovieByRegionId(string region);
        public Task<ResultService<MovieDTO>> GetInfoMoviesById(Guid id);
        public Task<ResultService<MovieDTO>> GetStatusMovie(string statusMovie);
        public Task<ResultService<MovieDTO>> Create(MovieDTO? movieDTO);
        public Task<ResultService<MovieDTO>> DeleteMovie(Guid guidId);
        public Task<ResultService<MovieDTO>> UpdateMovie(MovieDTO movieDTO);
        public Task<ResultService<MovieDTO>> UpdateMovieImgBackground(MovieDTO movieDTO);
    }
}
