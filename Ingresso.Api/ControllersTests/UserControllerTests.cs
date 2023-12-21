using Ingresso.Api.Controllers;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Ingresso.Application.Services;
using Ingresso.Domain.Authentication;
using Ingresso.Api.ControllersInterface;

namespace Ingresso.Api.ControllersTests
{
    public class UserControllerTests
    {
        private Mock<IBaseController> _baseController { get; }

        public UserControllerTests()
        {
            _baseController = new();
        }

        [Fact]
        public async void Should_Successfully_Create_New_User()
        {
            var currentUser = new Mock<ICurrentUser>();
            var userManagement = new Mock<IUserManagementService>();
            var userAuth = new Mock<IUserAuthenticationService>();
            var userConfirm = new Mock<IUserConfirmationService>();

            var userController = new UserController(currentUser.Object, userManagement.Object, userAuth.Object, userConfirm.Object, _baseController.Object);
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
            };

            userManagement.Setup(ser => ser.CreateAsync(It.IsAny<UserDto>())).ReturnsAsync(ResultService.Ok(new UserDto())); //arumar

            var result = await userController.CreateAsync(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Should_Failure_Create_New_User()
        {
            var currentUser = new Mock<ICurrentUser>();
            var userManagement = new Mock<IUserManagementService>();
            var userAuth = new Mock<IUserAuthenticationService>();
            var userConfirm = new Mock<IUserConfirmationService>();

            var userController = new UserController(currentUser.Object, userManagement.Object, userAuth.Object, userConfirm.Object, _baseController.Object);

            userManagement.Setup(ser => ser.CreateAsync(It.IsAny<UserDto>())).ReturnsAsync(ResultService.Fail<UserDto>("error"));

            var result = await userController.CreateAsync(new UserDto());

            var okResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
