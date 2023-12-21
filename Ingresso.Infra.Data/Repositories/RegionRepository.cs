using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Region?> GetRegionId(string region)
        {
            var regionObj = await
                _context.Regions
                .Where(x => x.City == region)
                .Select(x => new Region(x.Id))
                .FirstOrDefaultAsync();

            return regionObj;
        }

        public async Task<Region?> GetIdByNameCity(string state)
        {
            var regionObj = await 
                _context.Regions
                .Where(x => x.State == state)
                .Select(x => new Region(x.Id))
                .FirstOrDefaultAsync();

            return regionObj;
        }
    }
}
