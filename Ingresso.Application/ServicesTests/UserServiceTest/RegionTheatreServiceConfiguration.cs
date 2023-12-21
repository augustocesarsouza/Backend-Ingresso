using Ingresso.Domain.Repositories;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class RegionTheatreServiceConfiguration
    {
        public Mock<IRegionTheatreRepository> RegionTheatreRepository { get; }
        public Mock<IUnitOfWork> UnitOfWork { get; }

        public RegionTheatreServiceConfiguration()
        {
            RegionTheatreRepository = new();
            UnitOfWork = new();
        }
    }
}
