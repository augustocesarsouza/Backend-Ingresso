using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface ICinemaService
    {
        public Task<ResultService<CinemaDTO>> Create(CinemaDTO cinemaDTO);
    }
}
