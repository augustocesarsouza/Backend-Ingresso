using Ingresso.Domain.Entities;
using Ingresso.Infra.Data.Authentication;
using Xunit;

namespace Ingresso.Infra.Data.AuthenticationTests
{
    public class TokenGeneratorTests
    {
        [Fact]
        public void Generator_Token_Success()
        {
            // arrange
            var tokenGenerator = new TokenGeneratorEmail();
            var user = new User(10, "augusto@gmail.com", "88888888888", "ascascs");
            var userPermissionList = new List<UserPermission>();
            var permission = new Permission("admin", "administrator");
            var userPermission = new UserPermission(1, 10, permission);
            userPermissionList.Add(userPermission);

            // act
            var tokenValue = tokenGenerator.Generator(user, userPermissionList, "Augusto92456677");

            // assert
            Assert.NotNull(tokenValue.Data);
            Assert.NotNull(tokenValue.Data.Acess_Token);
            Assert.True(tokenValue.Data.Expirations > DateTime.Now);

            //Como estou garatindo que no userService eu não vou mandar um user ou userPermission null
            //Então não fiz o caso de insucesso da criação do token
        }
    }
}
