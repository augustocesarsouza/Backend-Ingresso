using Ingresso.Infra.Data.UtilityExternal.Interface;
using Ingresso.Domain.Repositories;
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
