using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IAdditionalInfoUserService
    {
        public Task<ResultService<AdditionalInfoUserDTO>> CreateInfo(AdditionalInfoUserDTO infoUserDTO);
    }
}
