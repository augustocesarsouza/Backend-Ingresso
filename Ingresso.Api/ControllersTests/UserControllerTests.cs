using Ingresso.Api.Controllers;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Ingresso.Application.Services;
using Ingresso.Domain.Authentication;
using Ingresso.Api.Authentication;

namespace Ingresso.Api.ControllersTests
{
    public class UserControllerTests
    {
        [Fact]
        public async void Should_Successfully_Create_New_User()
        {
            var userService = new Mock<IUserService>();
            var currentUser = new Mock<ICurrentUser>();
            var userController = new UserController(userService.Object, currentUser.Object);
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                EmailRecovery = "test123@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
                Phone = "67 981523696",
            };

            userService.Setup(ser => ser.CreateAsync(It.IsAny<UserDto>())).ReturnsAsync(ResultService.Ok(new UserDto()));

            var result = await userController.CreateAsync(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result); 

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Should_Failure_Create_New_User()
        {
            var userService = new Mock<IUserService>();
            var currentUser = new Mock<ICurrentUser>();
            var userController = new UserController(userService.Object, currentUser.Object);

            userService.Setup(ser => ser.CreateAsync(It.IsAny<UserDto>())).ReturnsAsync(ResultService.Fail<UserDto>("error"));

            var result = await userController.CreateAsync(new UserDto());

            var okResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
