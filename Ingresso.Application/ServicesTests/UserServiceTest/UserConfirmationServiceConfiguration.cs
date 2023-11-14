using Ingresso.Infra.Data.UtulityExternal.Interface;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Repositories;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserConfirmationServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICacheRedisUti> CacheRedisUtiMock { get; }

        public UserConfirmationServiceConfiguration()
        {
            UserRepositoryMock = new();
            UnitOfWorkMock = new();
            CacheRedisUtiMock = new();
        }
    }
}
