using Ingresso.Domain.Entities;
using Ingresso.Infra.Data.Authentication;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Ingresso.Infra.Data.AuthenticationTests
{
    public class TokenGeneratorCpfTests
    {
        [Fact]
        public void Generator_Token_Success()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "ascascasdcasdcascascascascascas" }
                })
                .Build();

            var tokenGenerator = new TokenGeneratorCpf(config);
            var guidId = Guid.NewGuid();
            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");
            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");

            var userPermission = new UserPermission(1, Guid.NewGuid(), permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.NotNull(tokenValue.Data.Acess_Token);
            Assert.True(tokenValue.Data.Expirations > DateTime.UtcNow);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_LessThan16_Length_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "sdsdsddsdsdsddd" }
                })
                .Build();

            var tokenGenerator = new TokenGeneratorCpf(config);
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
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Null_Cpf_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "your-secret-key-value" }
                })
                .Build();

            var tokenGenerator = new TokenGeneratorCpf(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", null, "ascascs");

            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Empty_Cpf_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "your-secret-key-value" }
                })
                .Build();
            var tokenGenerator = new TokenGeneratorCpf(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", "", "ascascs");

            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Null_Password_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "your-secret-key-value" }
                })
                .Build();
            var tokenGenerator = new TokenGeneratorCpf(config);
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "augusto@gmail.com", "88888888888", "ascascs");

            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, guidId, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, null);

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.False(tokenValue.IsSucess);
        }

        [Fact]
        public void When_Generating_Token_Should_Throw_Empty_Password_Error()
        {
            // arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "KeyJWT", "your-secret-key-value" }
                })
                .Build();
            var tokenGenerator = new TokenGeneratorCpf(config);
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
            var tokenGenerator = new TokenGeneratorCpf(config);
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
            var tokenGenerator = new TokenGeneratorCpf(config);
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
