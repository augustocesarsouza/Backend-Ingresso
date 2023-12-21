using AutoMapper;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class MovieServiceConfiguration
    {
        public Mock<IMovieRepository> MovieRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IMovieDTOValidator> MovieDTOValidatorMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }
        public Mock<IMovieTheaterService> MovieTheaterServiceMock { get; }
        public Mock<IRegionService> RegionServiceMock { get; }

        public MovieServiceConfiguration()
        {
            MovieRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            MovieDTOValidatorMock = new();
            CloudinaryUtiMock = new();
            MovieTheaterServiceMock = new();
            RegionServiceMock = new();
        }
    }
}


