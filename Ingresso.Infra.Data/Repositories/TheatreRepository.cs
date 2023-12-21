using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly ApplicationDbContext _ctx;

        public TheatreRepository(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public async Task<Theatre?> GetById(Guid guidId)
        {
            var theatreGet =
                await _ctx
                .Theatres
                .Where(x => x.Id == guidId)
                .Select(x => new Theatre(x.Id, x.Title))
                .FirstOrDefaultAsync();

            return theatreGet;
        }

        public async Task<Theatre?> GetByIdAllProps(Guid guid)
        {
            var movie = await _ctx
                .Theatres
                .Where(x => x.Id == guid)
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<List<Theatre>> GetAllTheatreByRegionId(Guid regionId)
        {
            var theatreGet =
                await _ctx.RegionTheatres
                .Where(r => r.RegionId == regionId)
                .Join(
                    _ctx.Theatres,
                    regionTheatre => regionTheatre.TheatreId,
                    theatre => theatre.Id,
                    (regionTheatre, theatre) => new Theatre(theatre.Id, theatre.Title, theatre.Data, theatre.Location, theatre.ImgUrl)
                    )
                .ToListAsync();


            return theatreGet;
        }

        public async Task<Theatre> Create(Theatre theatre)
        {
            await _ctx.Theatres.AddAsync(theatre);
            await _ctx.SaveChangesAsync();

            return theatre;
        }

        public async Task<Theatre> Delete(Theatre movie)
        {
            _ctx.Theatres.Remove(movie);
            await _ctx.SaveChangesAsync();
            return movie;
        }

        public async Task<Theatre> Update(Theatre theatre)
        {
            _ctx.Theatres.Update(theatre);
            await _ctx.SaveChangesAsync();
            return theatre;
        }
    }
}
