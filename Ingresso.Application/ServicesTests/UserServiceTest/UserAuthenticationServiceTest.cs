using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;
using Moq;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserAuthenticationServiceTest
    {
        private readonly UserAuthenticationServiceConfiguration _configuration;
        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTest()
        {
            _configuration = new();
            var userAuth = new UserAuthenticationService(
               _configuration.UserRepositoryMock.Object, _configuration.TokenGeneratorEmailMock.Object, _configuration.TokenGeneratorCpfMock.Object,
               _configuration.MapperMock.Object, _configuration.UnitOfWorkMock.Object, _configuration.UserPermissionServiceMock.Object,
               _configuration.PasswordHasherGeneratorMock.Object);

            _userAuthenticationService = userAuth;
        }

        [Fact]
        public async void Login_Not_Provide_Valid_Email_Or_Cpf()
        {
            var result = await _userAuthenticationService.Login("", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_Email_Without_Errors()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid?>())).ReturnsAsync(ResultService.Ok(new List<UserPermissionDTO>()));

            var tokenValue = new TokenOutValue();
            tokenValue.ValidateToken("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1));
            _configuration.TokenGeneratorEmailMock.Setup
                (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>(), It.IsAny<string>())).Returns(InfoErrors.Ok(tokenValue));

            var result = await _userAuthenticationService.Login("test@gmail.com", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GetCpf()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_VerifyPassword()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(false);

            var result = await _userAuthenticationService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GetAllPermissionUser()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);
            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid?>())).ReturnsAsync(ResultService.Fail<List<UserPermissionDTO>>("falha"));

            var result = await _userAuthenticationService.Login("augusto@gmail.com", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_EMAIL_Throw_Error_GenerateToken()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid>())).ReturnsAsync(ResultService.Ok(new List<UserPermissionDTO>()));

            _configuration.TokenGeneratorEmailMock.Setup
            (tok => tok.Generator
            (It.IsAny<User>(),
                It.IsAny<ICollection<UserPermission>>(), It.IsAny<string>())).Returns(InfoErrors.Fail(new TokenOutValue(), "erro"));

            var result = await _userAuthenticationService.Login("augusto@gmail.com", "Augusto92349923");
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
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid>())).ReturnsAsync(ResultService.Ok(permission));

            var tokenValue = new TokenOutValue();
            tokenValue.ValidateToken("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1));
            _configuration.TokenGeneratorCpfMock.Setup
                 (tok => tok.Generator(It.IsAny<User>(), It.IsAny<ICollection<UserPermission>>(), It.IsAny<string>())).Returns(InfoErrors.Ok(tokenValue));

            var result = await _userAuthenticationService.Login("88888888888", "Augusto92349923");
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GetCpf()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await _userAuthenticationService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_VerifyPassword()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(false);

            var result = await _userAuthenticationService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GetAllPermissionUser()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);
            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid?>())).ReturnsAsync(ResultService.Fail<List<UserPermissionDTO>>("falha"));

            var result = await _userAuthenticationService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Login_With_CPF_Throw_Error_GenerateToken()
        {
            _configuration.UserRepositoryMock.Setup(rep => rep.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User());

            _configuration.PasswordHasherGeneratorMock.Setup
                 (pass => pass.Verify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<char>(), It.IsAny<string>())).Returns(true);

            _configuration.UserPermissionServiceMock.Setup
                (ser => ser.GetAllPermissionUser(It.IsAny<Guid>())).ReturnsAsync(ResultService.Ok(new List<UserPermissionDTO>()));

            var tokenValue = new TokenOutValue();
            tokenValue.ValidateToken("sdfvsdfvsdfvsd", DateTime.Now.AddDays(1));

            _configuration.TokenGeneratorCpfMock.Setup
            (tok => tok.Generator
            (It.IsAny<User>(),
                 It.IsAny<ICollection<UserPermission>>(), It.IsAny<string>())).Returns(InfoErrors.Fail(new TokenOutValue(), "erro"));

            var result = await _userAuthenticationService.Login("88888888888", "Augusto92349923");
            Assert.False(result.IsSucess);
        }
    }
}
