using Ingresso.Application.Services;
using Ingresso.Domain.Entities;
using Moq;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class RegionTheatreServiceTest
    {
        private readonly RegionTheatreServiceConfiguration _regionTheatreServiceConfiguration;
        private readonly RegionTheatreService _regionTheatreService;

        public RegionTheatreServiceTest()
        {
            _regionTheatreServiceConfiguration = new();
            var regionTheatreService = new RegionTheatreService(_regionTheatreServiceConfiguration.RegionTheatreRepository.Object,
                _regionTheatreServiceConfiguration.UnitOfWork.Object);

            _regionTheatreService = regionTheatreService;
        }

        [Fact]
        public async void Should_Delete_RegionTheatre()
        {
            var regionTheatreList = new List<RegionTheatre>();
            var regionTheatre = new RegionTheatre();
            regionTheatre.SetTheatreAndRegionId(Guid.NewGuid(), Guid.NewGuid());
            regionTheatreList.Add(regionTheatre);

            _regionTheatreServiceConfiguration.RegionTheatreRepository.Setup(
                region => region.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(regionTheatreList);

            var result = await _regionTheatreService.Delete(Guid.NewGuid());
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Null_MovieTheaterList_BD()
        {
            _regionTheatreServiceConfiguration.RegionTheatreRepository.Setup(
                region => region.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync((List<RegionTheatre>?)null);

            var result = await _regionTheatreService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_List_LessThanEqual_Zero_MovieTheaterList_BD()
        {
            _regionTheatreServiceConfiguration.RegionTheatreRepository.Setup(
                region => region.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(new List<RegionTheatre>());

            var result = await _regionTheatreService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Excepetion_MovieTheaterList()
        {
            var regionTheatreList = new List<RegionTheatre>();
            var regionTheatre = new RegionTheatre();
            regionTheatre.SetTheatreAndRegionId(Guid.NewGuid(), Guid.NewGuid());
            regionTheatreList.Add(regionTheatre);

            _regionTheatreServiceConfiguration.RegionTheatreRepository.Setup(
                region => region.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(regionTheatreList);

            _regionTheatreServiceConfiguration.RegionTheatreRepository.Setup(
                region => region.Delete(It.IsAny<RegionTheatre>())).Throws(new Exception("test error"));

            var result = await _regionTheatreService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }
    }
}
