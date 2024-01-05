using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IAdditionalFoodMovieService
    {
        public Task<ResultService<List<AdditionalFoodMovieDTO>>> GetAllFoodMovie(Guid movieId);
        public Task<ResultService<AdditionalFoodMovieDTO>> Create(AdditionalFoodMovieDTO? additionalFoodMovieDTO);
    }
}
