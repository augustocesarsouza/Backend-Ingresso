using Ingresso.Application.DTOs;
using Ingresso.Domain.Entities;

namespace Ingresso.Application.Services.Interfaces
{
    public interface ICinemaMovieService
    {
        //public Task<ResultService<List<CinemaMovie>>> GetByRegionCinemaIdAndMovieId(string region, Guid movieId);
        public Task<ResultService<List<CinemaMovieDTO>>> GetByRegionCinemaIdAndMovieId(string region, Guid movieId);
        public Task<ResultService<CinemaMovieDTO>> Create(CinemaMovieDTO cinemaMovie);
    }
}
