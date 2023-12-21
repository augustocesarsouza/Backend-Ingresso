using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class RegionTheatreRepository : IRegionTheatreRepository
    {
        private readonly ApplicationDbContext _context;

        public RegionTheatreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegionTheatre> Create(RegionTheatre regionTheatre)
        {
            await _context.RegionTheatres.AddAsync(regionTheatre);
            await _context.SaveChangesAsync();

            return regionTheatre;
        }

        public async Task<List<RegionTheatre>?> GetByMovieId(Guid idTheatre)
        {
            var result = await _context
                .RegionTheatres.Where(x => x.TheatreId == idTheatre)
                .ToListAsync();

            return result;
        }

        public async Task<RegionTheatre> Delete(RegionTheatre movieTheater)
        {
            _context.RegionTheatres.Remove(movieTheater);
            await _context.SaveChangesAsync();

            return movieTheater;
        }
    }
}
