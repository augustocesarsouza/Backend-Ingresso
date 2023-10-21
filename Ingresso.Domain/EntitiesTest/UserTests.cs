using Ingresso.Domain;
using Ingresso.Domain.Entities;
using Xunit;

namespace Ingresso.Domain.EntitiesTest
{
    public class UserTests
    {
        [Fact]
        public void Validator_Token_User()
        {
            // arrange
            var user = new User();
            var validToken = "validToken";

            // act
            user.ValidatorToken(validToken);
            // assert
            Assert.Equal(validToken, user.Token);
        }

        [Fact]
        public void ValidatorToken_WhenInvalidTokenProvied_ThrowsException()
        {
            // arrange
            var user = new User();
            var valirdToken = "";

            // act and assert
            Assert.Throws<DomainValidationException>(() => user.ValidatorToken(valirdToken));
        }
    }
}