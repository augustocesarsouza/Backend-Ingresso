using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IAdditionalInfoUserRepository
    {
        public Task<AdditionalInfoUser> CreateInfo(AdditionalInfoUser infoUser);
    }
}
