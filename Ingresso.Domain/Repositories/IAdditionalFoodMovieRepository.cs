using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IAdditionalFoodMovieRepository
    {
        public Task<List<AdditionalFoodMovie>> GetAllFoodMovie(Guid movieId);
        public Task<AdditionalFoodMovie> Create(AdditionalFoodMovie additionalFoodMovie);
    }
}
