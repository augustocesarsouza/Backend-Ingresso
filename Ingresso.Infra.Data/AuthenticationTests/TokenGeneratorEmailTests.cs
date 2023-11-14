using Ingresso.Domain.Entities;
using Ingresso.Infra.Data.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Ingresso.Infra.Data.AuthenticationTests
{
    public class TokenGeneratorEmailTests
    {
        [Fact]
        public void Generator_Token_Success()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "ascascasdcasdcascascascascascas" } //Tratar a execeção ao mandar um valor de chame menor "ascasca"
                })
                .Build();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var guidId = Guid.NewGuid();
            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");
            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");

            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.NotNull(tokenValue.Data.Acess_Token);
            Assert.True(tokenValue.Data.Expirations > DateTime.UtcNow);

            //Como estou garatindo que no userService eu não vou mandar um user ou userPermission null
            //Então não fiz o caso de insucesso da criação do token
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_LessThan16_Length_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "sdsdsddsdsdsddd" } //Tratar a execeção ao mandar um valor de chame menor "ascasca"
                })
                .Build();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");
            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");

            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);

            //Como estou garatindo que no userService eu não vou mandar um user ou userPermission null
            //Então não fiz o caso de insucesso da criação do token
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Null_Email_Error()
        {
            // arrange
            var config = new ConfigurationBuilder().Build();
            var tokenGenerator = new TokenGeneratorEmail(config);
            var guidId = Guid.NewGuid();


            var user = new User(guidId, null, "88888888888", "ascascs");

            // act
            var tokenValue = tokenGenerator.Generator(user, new List<UserPermission>(), "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Empty_Email_Error()
        {
            // arrange
            var config = new ConfigurationBuilder().Build();
            var guidId = Guid.NewGuid();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var user = new User(guidId, "", "88888888888", "ascascs");

            // act
            var tokenValue = tokenGenerator.Generator(user, new List<UserPermission>(), "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Null_Password_Error()
        {
            // arrange
            var config = new ConfigurationBuilder().Build();
            var guidId = Guid.NewGuid();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");

            // act
            var tokenValue = tokenGenerator.Generator(user, new List<UserPermission>(), null);

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Empty_Password_Error()
        {
            // arrange
            var config = new ConfigurationBuilder().Build();
            var guidId = Guid.NewGuid();

            var tokenGenerator = new TokenGeneratorCpf(config);
            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");

            // act
            var tokenValue = tokenGenerator.Generator(user, new List<UserPermission>(), "");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Null_KeySecretJwt_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", null! }
                })
                .Build();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");

            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92349923");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Empty_KeySecretJwt_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "" }
                })
                .Build();

            var tokenGenerator = new TokenGeneratorEmail(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");

            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }
    }
}
