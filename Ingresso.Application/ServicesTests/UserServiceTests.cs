using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Moq;
using Xunit;

namespace Ingresso.Application.ServicesTests
{
    public class UserServiceTests
    {
        private readonly UserServiceConfiguration _configuration;

        public UserServiceTests()
        {
            _configuration = new UserServiceConfiguration();
        }

        [Fact]
        public async void Must_Create_An_Account()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            var userDto = new UserDto
            {
                Name = "Test",
                Email = "test@gmail.com",
                EmailRecovery = "test123@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
                Phone = "67 981523696",
            };

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(default(User));
            _configuration.UserRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(new User());

            // Act
            var result = await userService.CreateAsync(userDto);

            // assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void CreateAsync_WithInvalidDTOReturnErrorMessageResult()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            var userDto = new UserDto
            {
                Name = "Test",
                Email = "test@gmail.com",
                EmailRecovery = "",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
                Phone = "67 981523696",
            };

            var result = await userService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Email_Without_Errors()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            //var user = new User(1, "augusto", "sdfvsfdvsdv", "augusto@gmail.com");

            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup(pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(new ResultService<List<UserPermissionDTO>>());
            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1)));

            var result = await userService.Login("test@gmail.com", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Email_Throw_Login_Error()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup(pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(new ResultService<List<UserPermissionDTO>>());
            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1)));

            var result = await userService.Login("test@gmail.com", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Cpf_Without_Errors()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup(pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var permission = new List<UserPermissionDTO>();
            permission.Add(new UserPermissionDTO { UserId = 1, PermissionId = 1 });

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int>())).ReturnsAsync(ResultService.Ok(permission));

            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1)));

            var result = await userService.Login("88888888888", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Cpf_With_Erro_GetCpf()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Cpf_With_Erro_Verify_Password()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            _configuration.PasswordHasherGeneratorMock.Setup(pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var result = await userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Cpf_With_Erro_GetAllPermissionUser()
        {
            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object
                );

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(ResultService.Fail<List<UserPermissionDTO>>("falha"));

            var result = await userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

    }
}

//namespace Ingresso.Domain.Authentication
//{
//    public class TokenOutValue
//    {
//        public string? Acess_Token { get; set; }
//        public DateTime Expirations { get; set; }
//    }
//}
