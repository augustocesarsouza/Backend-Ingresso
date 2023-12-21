using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MovieTheater?> GetById(Guid guidId)
        {
            var result = await _context
                .MovieTheaters.Where(x => x.Id == guidId)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<MovieTheater>?> GetByMovieId(Guid idMovie)
        {
            var result = await _context
                .MovieTheaters.Where(x => x.MovieId == idMovie)
                .ToListAsync();

            return result;
        }

        public async Task<MovieTheater> Delete(MovieTheater movieTheater)
        {
            _context.MovieTheaters.Remove(movieTheater);
            await _context.SaveChangesAsync();

            return movieTheater;
        }
    }
}
