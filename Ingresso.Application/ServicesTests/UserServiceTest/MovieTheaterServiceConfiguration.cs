using AutoMapper;
using Ingresso.Domain.Repositories;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class MovieTheaterServiceConfiguration
    {
        public Mock<IMovieTheaterRepository> MovieTheaterRepository { get; }
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public Mock<IMapper> Mapper { get; }

        public MovieTheaterServiceConfiguration()
        {
            MovieTheaterRepository = new();
            UnitOfWork = new();
            Mapper = new();
        }
    }
}
