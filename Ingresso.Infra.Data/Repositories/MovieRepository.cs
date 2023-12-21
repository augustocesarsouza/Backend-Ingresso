using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie?> GetById(Guid guid)
        {
            var movie = await _context
                .Movies
                .Where(x => x.Id == guid)
                .Select(x => new Movie(x.Id, x.Title))
                .FirstOrDefaultAsync();

            return movie;
        }


        public async Task<Movie?> GetInfoMoviesById(Guid guid)
        {
            var movie = await _context
                .Movies
                .Where(x => x.Id == guid)
                .Select(x => new Movie(x.Id ,x.Title, x.Description, x.Gender, x.Duration, x.MovieRating, x.ImgUrl, x.ImgUrlBackground))
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<Movie?> GetStatusMovie(string statusMovie)
        {
            var movie = await _context
                .Movies
                .Where(x => x.StatusMovie == statusMovie)
                .Select(x => new Movie(x.Id, x.Title, x.Description, x.Gender, x.Duration, x.MovieRating, x.ImgUrl, x.StatusMovie))
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<Movie?> GetByIdAllProps(Guid guid)
        {
            var movie = await _context
                .Movies
                .Where(x => x.Id == guid)
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<List<Movie>> GetAllMovieByRegionId(Guid regionId)
        {
            var theatreGet =
                await _context.MovieTheaters
                .Where(m => m.RegionId == regionId)
                .Join(
                    _context.Movies,
                    movieTheaters => movieTheaters.MovieId,
                    movie => movie.Id,
                    (movieTheaters, movie) => new Movie(movie.Id, movie.Title, movie.ImgUrl, movie.MovieRating))
                .ToListAsync();


            return theatreGet;
        }

        public async Task<Movie?> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Update(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
