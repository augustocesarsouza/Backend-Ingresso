using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IAdditionalInfoUserService
    {
        public Task<ResultService<AdditionalInfoUserDTO>> GetInfoUser(string idGuid);
        public Task<ResultService<AdditionalInfoUserDTO>> CreateInfo(AdditionalInfoUserDTO infoUserDTO);
        public Task<ResultService<AdditionalInfoUserDTO>> UpdateAsync(AdditionalInfoUserDTO infoUser, string password);
    }
}
