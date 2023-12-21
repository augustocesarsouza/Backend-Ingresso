using FluentValidation.Results;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Domain.Entities;
using Ingresso.Infra.Data.CloudinaryConfigClass;
using Moq;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class MovieServiceTest
    {
        private readonly MovieServiceConfiguration _movieServiceConfiguration;
        private readonly MovieService _movieService;

        public MovieServiceTest()
        {
            _movieServiceConfiguration = new();
            var movieServide = new MovieService(_movieServiceConfiguration.MovieRepositoryMock.Object, _movieServiceConfiguration.MapperMock.Object,
                _movieServiceConfiguration.UnitOfWorkMock.Object, _movieServiceConfiguration.MovieDTOValidatorMock.Object,
                _movieServiceConfiguration.CloudinaryUtiMock.Object, _movieServiceConfiguration.MovieTheaterServiceMock.Object,
                _movieServiceConfiguration.RegionServiceMock.Object);

            _movieService = movieServide;
        }

        [Fact]
        public async void Should_Get_All_Movie_By_RegionId_Success()
        {
            string region = "São paulo";

            _movieServiceConfiguration.RegionServiceMock.Setup(rep => rep.GetIdByNameCity(It.IsAny<string>())).ReturnsAsync(ResultService.Ok(new RegionDTO()));

            _movieServiceConfiguration.MovieRepositoryMock.Setup(ser => ser.GetAllMovieByRegionId(It.IsAny<Guid>())).ReturnsAsync(new List<Movie>());

            var result = await _movieService.GetAllMovieByRegionId(region);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Get_All_Movie_By_RegionId_Error()
        {
            string region = "São paulo";

            _movieServiceConfiguration.RegionServiceMock.Setup(rep => rep.GetIdByNameCity(It.IsAny<string>())).ReturnsAsync(ResultService.Fail((RegionDTO?)null));

            var result = await _movieService.GetAllMovieByRegionId(region);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Get_Status_Movie_Success()
        {
            string statusMovie = "Highlight";

            _movieServiceConfiguration.MovieRepositoryMock.Setup(rep => rep.GetStatusMovie(It.IsAny<string>())).ReturnsAsync(new Movie());

            var result = await _movieService.GetStatusMovie(statusMovie);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Get_Status_Movie_Error()
        {
            string statusMovie = "Highlight";

            _movieServiceConfiguration.MovieRepositoryMock.Setup(rep => rep.GetStatusMovie(It.IsAny<string>())).ReturnsAsync((Movie?)null);

            var result = await _movieService.GetStatusMovie(statusMovie);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Movie_Whithout_Error()
        {
            _movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            _movieServiceConfiguration.MovieRepositoryMock.Setup(rep => rep.Create(It.IsAny<Movie>())).ReturnsAsync(new Movie());

            var result = await _movieService.Create(new MovieDTO());
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Movie_Error()
        {
            var result = await _movieService.Create(null);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_When_Passing_Invalid_DTO_To_Create_Movie()
        {
            _movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                new ValidationFailure("PropertyName", "Error message 1"),
                }));

            var result = await _movieService.Create(new MovieDTO());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_In_Return_Create_DB_Movie()
        {
            var validationResult = new ValidationResult();

            _movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            _movieServiceConfiguration.MovieRepositoryMock.Setup(rep => rep.Create(It.IsAny<Movie>())).ReturnsAsync((Movie?)null);

            var result = await _movieService.Create(new MovieDTO());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_When_ThrowErro_Create_DB_Movie()
        {
            var validationResult = new ValidationResult();

            _movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            _movieServiceConfiguration.MovieRepositoryMock.Setup(rep => rep.Create(It.IsAny<Movie>())).Throws(new Exception("Simulated database exception"));

            var result = await _movieService.Create(new MovieDTO());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Delete_Movie_With_Success()
        {
            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            _movieServiceConfiguration.MovieTheaterServiceMock.Setup(
                ser => ser.Delete(It.IsAny<Guid>())).ReturnsAsync(ResultService.Ok(new MovieTheaterDTO()));

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.Delete(It.IsAny<Movie>())).ReturnsAsync(new Movie());

            var result = await _movieService.DeleteMovie(Guid.NewGuid());

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Erro_When_Get_Movie_DB_Movie_When_Delete()
        {
            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync((Movie?)null);

            var result = await _movieService.DeleteMovie(Guid.NewGuid());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Erro_When_Delete_MovieTheater()
        {
            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            _movieServiceConfiguration.MovieTheaterServiceMock.Setup(
                movThe => movThe.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(ResultService.Fail<MovieTheaterDTO>("Algum erro na hora de deletar verifique"));

            var result = await _movieService.DeleteMovie(Guid.NewGuid());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Exception_When_Delete_Movie()
        {
            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            _movieServiceConfiguration.MovieTheaterServiceMock.Setup(
                 ser => ser.Delete(It.IsAny<Guid>())).ReturnsAsync(ResultService.Ok(new MovieTheaterDTO()));

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.Delete(It.IsAny<Movie>())).Throws(new Exception("Simulated database exception"));

            var result = await _movieService.DeleteMovie(Guid.NewGuid());

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Update_Movie_With_Success()
        {
            var movieDto = new MovieDTO 
            {
                Id = Guid.Parse("a752bfcc-0da4-4c7d-b1a7-9996c81cb3a3"),
                Base64Img = "ascascasc"
            };

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(
                cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Id_Null_Update_Movie()
        {
            var movieDto = new MovieDTO
            {
                Id = null
            };

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Base64Img_Null_Update_Movie()
        {
            var movieDto = new MovieDTO
            {
                Id = Guid.Parse("a752bfcc-0da4-4c7d-b1a7-9996c81cb3a3"),
                Base64Img = null
            };

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_MovieUpdated_Null_Update_Movie()
        {
            var movieDto = new MovieDTO
            {
                Id = Guid.Parse("a752bfcc-0da4-4c7d-b1a7-9996c81cb3a3"),
                Base64Img = "ascascasc"
            };

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync((Movie?)null);

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Result_Create_ImgCloudinary_Null_ImgUrl_Update_Movie()
        {
            var movieDto = new MovieDTO
            {
                Id = Guid.Parse("a752bfcc-0da4-4c7d-b1a7-9996c81cb3a3"),
                Base64Img = "ascascasc"
            };

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = null,
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(
                cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Throw_Error_Result_Create_ImgCloudinary_Null_PublicId_Update_Movie()
        {
            var movieDto = new MovieDTO
            {
                Id = Guid.Parse("a752bfcc-0da4-4c7d-b1a7-9996c81cb3a3"),
                Base64Img = "ascascasc"
            };

            _movieServiceConfiguration.MovieRepositoryMock.Setup(
                rep => rep.GetByIdAllProps(It.IsAny<Guid>())).ReturnsAsync(new Movie());

            var cloduinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = null,
            };

            _movieServiceConfiguration.CloudinaryUtiMock.Setup(
                cloud => cloud.CreateImg(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(cloduinary);

            var result = await _movieService.UpdateMovie(movieDto);

            Assert.False(result.IsSucess);
        }
    }
}
