using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IAdditionalInfoUserRepository
    {
        public Task<AdditionalInfoUser> CreateInfo(AdditionalInfoUser infoUser);
        public Task<AdditionalInfoUser?> GetInfoUser(Guid idGuid);
        public Task<AdditionalInfoUser?> GetByIdGuidUser(Guid idGuid);
        public Task<AdditionalInfoUser> UpdateAsync(AdditionalInfoUser infoUser);
    }
}
