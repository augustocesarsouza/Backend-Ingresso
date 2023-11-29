using Ingresso.Application.Services;
using Ingresso.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserConfirmationServiceTest
    {
        private readonly UserConfirmationServiceConfiguration _userConfirmationConfig;
        private readonly UserConfirmationService _userConfirmationService;

        public UserConfirmationServiceTest()
        {
            _userConfirmationConfig = new();
            var userConfigServi = new UserConfirmationService(
                _userConfirmationConfig.UserRepositoryMock.Object, _userConfirmationConfig.UnitOfWorkMock.Object, _userConfirmationConfig.CacheRedisUtiMock.Object);

            _userConfirmationService = userConfigServi;
        }

        [Fact]
        public async void Should_Confirm_Token_Valid_Creation()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var expires = DateTime.UtcNow.AddMinutes(10);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            _userConfirmationConfig.CacheRedisUtiMock.Setup(dis =>
            dis.GetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync("string");

            _userConfirmationConfig.UserRepositoryMock.Setup(rep => rep.GetUserById(It.IsAny<Guid>())).ReturnsAsync(new User());

            _userConfirmationConfig.UserRepositoryMock.Setup(rep => rep.UpdateUser(It.IsAny<User>())).ReturnsAsync(new User());

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.True(result.IsSucess);
        }

        [Fact]
        [Description("The idea here is if I send anything different in the Claim in 'id' that is not a Guid it will throw an error")]
        public async void Should_Throw_Erro_When_Sending_Id_Different_From_Guid()
        {
            var claims = new List<Claim>
                    {
                    new Claim("id", "10"),
                    };

            var expires = DateTime.UtcNow.AddMinutes(10);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Token_Invalid_Confirm_Token_Expired()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var expires = DateTime.UtcNow.AddMilliseconds(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Invalid_Was_Not_Reported_Claims_IdUser()
        {
            var claims = new List<Claim>
            {
            };

            var expires = DateTime.UtcNow.AddMilliseconds(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Invalid_Was_Not_Reported_Claims_Exp()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Passing_Invalid_Token_Value()
        {
            var invalidToken = "token_invalido_aqui";

            var result = await _userConfirmationService.GetConfirmToken(invalidToken);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Already_Viewed_Once_Token()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var expires = DateTime.UtcNow.AddMinutes(10);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            _userConfirmationConfig.CacheRedisUtiMock.Setup(dis =>
            dis.GetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(string.Empty);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.Equal(1, result?.Data?.TokenAlreadyVisualized);
        }

        [Fact]
        public async void DataBase_Returns_User_Null_GetId()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var expires = DateTime.UtcNow.AddMinutes(10);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            _userConfirmationConfig.CacheRedisUtiMock.Setup(dis =>
            dis.GetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(tokenTest);

            _userConfirmationConfig.UserRepositoryMock.Setup(rep => rep.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void DataBase_Returns_User_Null_Update()
        {
            var idUserGid = Guid.NewGuid().ToString();

            var claims = new List<Claim>
                    {
                    new Claim("id", idUserGid),
                    };

            var expires = DateTime.UtcNow.AddMinutes(10);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
            var tokenValidate = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
                claims: claims);

            var tokenTest = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

            _userConfirmationConfig.CacheRedisUtiMock.Setup(dis =>
            dis.GetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(tokenTest);

            _userConfirmationConfig.UserRepositoryMock.Setup(rep => rep.GetUserById(It.IsAny<Guid>())).ReturnsAsync(new User());

            _userConfirmationConfig.UserRepositoryMock.Setup(rep => rep.UpdateUser(It.IsAny<User>())).ReturnsAsync((User?)null);

            var result = await _userConfirmationService.GetConfirmToken(tokenTest);
            Assert.False(result.IsSucess);
        }
    }
}
