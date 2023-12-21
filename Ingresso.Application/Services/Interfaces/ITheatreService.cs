using Ingresso.Application.DTOs;
namespace Ingresso.Application.Services.Interfaces
{
    public interface ITheatreService
    {
        public Task<ResultService<List<TheatreDTO>>> GetAllTheatreByRegionId(string region);
        public Task<ResultService<TheatreDTO>> Create(TheatreDTO theatreDTO);
        public Task<ResultService<TheatreDTO>> DeleteTheatre(Guid guidId);
        public Task<ResultService<TheatreDTO>> UpdateMovie(TheatreDTO theatreDTO);
    }
}
