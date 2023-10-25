using Ingresso.Domain.Authentication;
using Xunit;

namespace Ingresso.Domain.AuthenticationTests
{
    public class TokenOutValueTests
    {
        [Fact]
        public void ValidateToken_Must_Not_Be_Null_AcessToken()
        {
            // arrange
            var tokenValue = new TokenOutValue();
            var acessToken = "ascascascascs";
            var expirations = DateTime.Now.AddDays(1);

            // act
            tokenValue.ValidateToken(acessToken, expirations);

            // assert
            Assert.Equal(acessToken, tokenValue.Acess_Token);
        }

        [Fact]
        public void ValidateToken_Must_Be_Null_AcessToken()
        {
            // arrange
            var tokenValue = new TokenOutValue();
            var expirations = DateTime.Now.AddDays(1);

            // act
            var sucessfullyCreatedToken = tokenValue.ValidateToken(null, expirations);

            // assert
            Assert.False(sucessfullyCreatedToken);
        }

        [Fact]
        public void ValidateToken_Must_Be_Empty_AcessToken()
        {
            // arrange
            var tokenValue = new TokenOutValue();
            var acessToken = "";
            var expirations = DateTime.Now.AddDays(1);

            // act
            var sucessfullyCreatedToken = tokenValue.ValidateToken(acessToken, expirations);

            // assert
            Assert.False(sucessfullyCreatedToken);
        }
    }
}
