using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IRegionService
    {
        public Task<ResultService<RegionDTO>> GetRegionId(string state);
        public Task<ResultService<RegionDTO>> GetIdByNameCity(string state);
    }
}
