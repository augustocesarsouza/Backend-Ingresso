using CloudinaryDotNet.Actions;
using Ingresso.Application.Services;
using Ingresso.Domain.Entities;
using Moq;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class MovieTheaterServiceTest
    {
        private readonly MovieTheaterServiceConfiguration _movieTheaterServiceConfiguration;
        private readonly MovieTheaterService _movieTheaterService;

        public MovieTheaterServiceTest()
        {
            _movieTheaterServiceConfiguration = new();
            var movieTheaterService = new MovieTheaterService(_movieTheaterServiceConfiguration.MovieTheaterRepository.Object,
                _movieTheaterServiceConfiguration.UnitOfWork.Object, _movieTheaterServiceConfiguration.Mapper.Object);

            _movieTheaterService = movieTheaterService;
        }

        [Fact]
        public async void Should_Get_By_Id_Movie_Successfully()
        {
            //_movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
            //    movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync((List<MovieTheater>?)null);

            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(new List<MovieTheater>());

            var result = await _movieTheaterService.GetByIdMovie(Guid.NewGuid());
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Get_By_Id_Movie_Not_Success()
        {
            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync((List<MovieTheater>?)null);

            var result = await _movieTheaterService.GetByIdMovie(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Delete_Successfully()
        {
            var listMovieThe = new List<MovieTheater>();
            var movieTheater = new MovieTheater();
            movieTheater.AddMovieIdAndRegionId(Guid.NewGuid(), Guid.NewGuid());
            listMovieThe.Add(movieTheater);

            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(listMovieThe);

            var result = await _movieTheaterService.Delete(Guid.NewGuid());
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_List_Movie_Theater_Delete_Null()
        {
            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync((List<MovieTheater>?)null);

            var result = await _movieTheaterService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_List_Movie_Theater_Delete_Empty()
        {
            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(new List<MovieTheater>());

            var result = await _movieTheaterService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Exception_Delete()
        {
            var listMovieThe = new List<MovieTheater>();
            var movieTheater = new MovieTheater();
            movieTheater.AddMovieIdAndRegionId(Guid.NewGuid(), Guid.NewGuid());
            listMovieThe.Add(movieTheater);

            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieTheater => movieTheater.GetByMovieId(It.IsAny<Guid>())).ReturnsAsync(listMovieThe);

            _movieTheaterServiceConfiguration.MovieTheaterRepository.Setup(
                movieThea => movieThea.Delete(It.IsAny<MovieTheater>())).Throws(new Exception("Error test"));

            var result = await _movieTheaterService.Delete(Guid.NewGuid());
            Assert.False(result.IsSucess);
        }
    }
}
