using Ingresso.Api.Authentication;
using Ingresso.Api.Controllers;
using Ingresso.Api.ControllersInterface;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ingresso.Api.ControllersTests
{
    public class MovieControllerTest
    {
        private Mock<IMovieService> _movieService { get; }
        private Mock<ICurrentUser> _currentUser { get; }
        private Mock<IBaseController> _baseController { get; }
        private readonly MovieController _movieController;

        public MovieControllerTest()
        {
            _movieService = new Mock<IMovieService>();
            _currentUser = new Mock<ICurrentUser>();
            _baseController = new Mock<IBaseController>();

            var movieController = new MovieController(_movieService.Object, _currentUser.Object, _baseController.Object);
            _movieController = movieController;
        }

        [Fact]
        public async void Should_Get_All_Movie_By_RegionId_Successfully()
        {
            _movieService.Setup(ser => ser.GetAllMovieByRegionId(It.IsAny<string>())).ReturnsAsync(ResultService.Ok(new List<MovieDTO>()));

            var region = "São Paulo";

            var result = await _movieController.GetAllMovieByRegionId(region);
            var okResul = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResul.StatusCode);
        }

        [Fact]
        public async void Should_Get_All_Movie_By_RegionId_Error_400()
        {
            _movieService.Setup(ser => ser.GetAllMovieByRegionId(It.IsAny<string>())).ReturnsAsync(ResultService.Fail<List<MovieDTO>>("erro"));

            var region = "São Paulo";

            var result = await _movieController.GetAllMovieByRegionId(region);
            var okResul = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, okResul.StatusCode);
        }

        [Fact]
        public async void Should_Get_Status_Movie_Status_200()
        {
            //_currentUser.SetupGet(c => c.Password).Returns("password");

            //_baseController.Setup(baseC => baseC.Validator(_currentUser.Object)).Returns(new UserAuthDTO());

            _movieService.Setup(ser => ser.GetStatusMovie(It.IsAny<string>())).ReturnsAsync(ResultService.Ok<MovieDTO>("ok"));

            var statusMovie = "Highlight";

            var result = await _movieController.GetStatusMovie(statusMovie);
            var okResul = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResul.StatusCode);
        }

        //[Fact]
        //public async void Should_Get_Status_Movie_Status_403()
        //{
        //    //var obj = new
        //    //{
        //    //    code = "acesso_negado",
        //    //    message = "Usuario não contem as devidas informações necessarias para acesso"
        //    //};

        //    //var forbi = new ObjectResult(obj) { StatusCode = 403 };

        //    //_baseController.Setup(baseC => baseC.Forbidden()).Returns(forbi);

        //    var statusMovie = "Highlight";

        //    var result = await _movieController.GetStatusMovie(statusMovie);
        //    var okResul = Assert.IsType<ObjectResult>(result);

        //    Assert.Equal(403, okResul.StatusCode);
        //}

        [Fact]
        public async void Should_Get_Status_Movie_Status_400_Bad()
        {
            //_currentUser.SetupGet(c => c.Password).Returns("password");

            //_baseController.Setup(baseC => baseC.Validator(_currentUser.Object)).Returns(new UserAuthDTO());

            _movieService.Setup(ser => ser.GetStatusMovie(It.IsAny<string>())).ReturnsAsync(ResultService.Fail<MovieDTO>("fail"));

            var statusMovie = "Highlight";

            _movieService.Setup(ser => ser.GetStatusMovie(It.IsAny<string>())).ReturnsAsync(ResultService.Fail<MovieDTO>("test error"));

            var result = await _movieController.GetStatusMovie(statusMovie);
            var okResul = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, okResul.StatusCode);
        }

        [Fact]
        public async void Should_Create_Successfully()
        {
            _movieService.Setup(ser => ser.Create(It.IsAny<MovieDTO>())).ReturnsAsync(ResultService.Ok<MovieDTO>("tudo certo"));

            var result = await _movieController.Create(new MovieDTO());
            var okResul = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResul.StatusCode);
        }

        [Fact]
        public async void Should_Create_Fail()
        {
            _movieService.Setup(ser => ser.Create(It.IsAny<MovieDTO>())).ReturnsAsync(ResultService.Fail<MovieDTO>("falha ao criar"));

            var result = await _movieController.Create(new MovieDTO());
            var okResul = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, okResul.StatusCode);
        }
    }
}
