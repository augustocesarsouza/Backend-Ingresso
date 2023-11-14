using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IUserConfirmationService
    {
        public Task<ResultService<TokenAlreadyVisualizedDTO>> GetConfirmToken(string token);
    }
}
