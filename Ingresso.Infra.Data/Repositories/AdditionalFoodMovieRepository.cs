using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class AdditionalFoodMovieRepository : IAdditionalFoodMovieRepository
    {
        private readonly ApplicationDbContext _ctx;

        public AdditionalFoodMovieRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<AdditionalFoodMovie>> GetAllFoodMovie(Guid movieId)
        {
            var foods = await _ctx
                .AdditionalFoodMovie
                .Where(x => x.MovieId == movieId)
                .Select(x => new AdditionalFoodMovie(x.Title, x.Price, x.Fee, x.ImgUrl))
                .ToListAsync();

            return foods;
        }


        public async Task<AdditionalFoodMovie> Create(AdditionalFoodMovie additionalFoodMovie)
        {
            await _ctx.AdditionalFoodMovie.AddAsync(additionalFoodMovie);
            await _ctx.SaveChangesAsync();
            return additionalFoodMovie;
        }
    }
}
