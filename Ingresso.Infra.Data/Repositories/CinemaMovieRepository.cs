using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class CinemaMovieRepository : ICinemaMovieRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CinemaMovieRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<CinemaMovie>> GetByRegionCinemaIdAndMovieId(Guid regionId, Guid movieId)
        {
            var get = await _ctx.CinemaMovies
                .Where(x => x.RegionId == regionId && x.MovieId == movieId)
                .Select(x => new CinemaMovie(new Cinema(x.Cinema.NameCinema, x.Cinema.District, x.Cinema.Ranking), x.ScreeningSchedule))
                .ToListAsync();

            return get;
        }

        public async Task<CinemaMovie> Create(CinemaMovie cinemaMovie)
        {
            await _ctx.CinemaMovies.AddAsync(cinemaMovie);
            await _ctx.SaveChangesAsync();

            return cinemaMovie;
        }
    }
}
