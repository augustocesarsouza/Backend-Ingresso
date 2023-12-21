using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IRegionRepository
    {
        public Task<Region?> GetRegionId(string region);
        public Task<Region?> GetIdByNameCity(string state);
    }
}
