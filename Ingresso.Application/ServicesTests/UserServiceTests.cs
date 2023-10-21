using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Moq;
using Xunit;
using FluentValidation.Results;

namespace Ingresso.Application.ServicesTests
{
    public class UserServiceTests
    {
        private readonly UserServiceConfiguration _configuration;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _configuration = new UserServiceConfiguration();

            var userService = new UserService(
                _configuration.UserRepositoryMock.Object,
                _configuration.TokenGeneratorMock.Object,
                _configuration.MapperMock.Object,
                _configuration.UnitOfWorkMock.Object,
                _configuration.UserPermissionServiceMock.Object,
                _configuration.PasswordHasherGeneratorMock.Object,
                _configuration.UserCreateDTOValidator.Object
                );

            _userService = userService;
        }

        [Fact]
        public async void Should_Create_Account_Without_Errors()
        {
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

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateUserDto(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User?)null);
            _configuration.UserRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(new User());

            // Act
            var result = await _userService.CreateAsync(userDto);

            // assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_DTO_Null()
        {
            var result = await _userService.CreateAsync(null);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_ValidateUserDto()
        {
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

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateUserDto(It.IsAny<UserDto>()))
            .Returns(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("PropertyName", "Error message 1"),
            }));

            // Act
            var result = await _userService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_CheckUserExits()
        {
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

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateUserDto(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup
                (repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User());

            // Act
            var result = await _userService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_ThrowException()
        {
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

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateUserDto(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User?)null);

            _configuration.UserRepositoryMock.Setup
                (repo => repo.CreateAsync(It.IsAny<User>())).Throws(new Exception("Simulated database exception"));

            // Act
            var result = await _userService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Login_Not_Provide_Valid_Email_Or_Cpf()
        {
            var result = await _userService.Login("", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Email_Without_Errors()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(new ResultService<List<UserPermissionDTO>>());

            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1)));

            var result = await _userService.Login("test@gmail.com", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GetCpf()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await _userService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_VerifyPassword()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(false);

            var result = await _userService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GetAllPermissionUser()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(ResultService.Fail<List<UserPermissionDTO>>("falha"));

            var result = await _userService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GenerateToken()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int>())).ReturnsAsync(ResultService.Ok(new List<UserPermissionDTO>()));

            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("", DateTime.Now.AddDays(1)));

            var result = await _userService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }


        [Fact]
        public async void Should_Login_With_CPF_Without_Errors()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            var permission = new List<UserPermissionDTO>();
            permission.Add(new UserPermissionDTO { UserId = 1, PermissionId = 1 });

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int>())).ReturnsAsync(ResultService.Ok(permission));

            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1)));

            var result = await _userService.Login("88888888888", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GetCpf()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await _userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_VerifyPassword()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(false);

            var result = await _userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GetAllPermissionUser()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int?>())).ReturnsAsync(ResultService.Fail<List<UserPermissionDTO>>("falha"));

            var result = await _userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GenerateToken()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                 (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<int>())).ReturnsAsync(ResultService.Ok(new List<UserPermissionDTO>()));

            _configuration.TokenGeneratorMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>())).Returns(new TokenOutValue("", DateTime.Now.AddDays(1)));

            var result = await _userService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }
    }
}