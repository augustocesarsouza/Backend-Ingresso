using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace Ingresso.Infra.Data.SendEmailUser
{
    public class SendEmailUserTest
    {
        private readonly SendEmailUserConfiguration _sendEmailUserConfiguration;
        private readonly SendEmailUser _sendEmailUser;

        public SendEmailUserTest()
        {
            _sendEmailUserConfiguration = new();
            var sendEmailUser = new SendEmailUser(_sendEmailUserConfiguration.CacheRedisUti.Object, _sendEmailUserConfiguration.SendEmailBrevo.Object);
            _sendEmailUser = sendEmailUser;
        }

        [Fact]
        public async void Should_Send_Email_Successfully() // Trocar para ingles
        {
            var userId = Guid.NewGuid();
            var user = new User(userId, "augusto");

            _sendEmailUserConfiguration.CacheRedisUti.Setup(
                ca => ca.GetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync("");

            _sendEmailUserConfiguration.CacheRedisUti.Setup(
                ca => ca.SetStringAsyncWrapper(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()));

            _sendEmailUserConfiguration.SendEmailBrevo.Setup(
                send => send.SendEmail(It.IsAny<User>(), It.IsAny<string>())).Returns(InfoErrors.Ok("tudo certo"));

            var result = await _sendEmailUser.SendEmail(user);

            Assert.True(result.IsSucess);
        }
    }
}
