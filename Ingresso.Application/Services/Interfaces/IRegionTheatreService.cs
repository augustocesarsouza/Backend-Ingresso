using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IRegionTheatreService
    {
        public Task<ResultService<RegionTheatreDTO>> Delete(Guid idMovie);
    }
}
