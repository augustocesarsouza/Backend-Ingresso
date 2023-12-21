using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;

namespace Ingresso.Infra.Data.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CinemaRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Cinema> Create(Cinema cinema)
        {
            await _ctx.Cinemas.AddAsync(cinema);
            await _ctx.SaveChangesAsync();
            return cinema;
        }
    }
}
