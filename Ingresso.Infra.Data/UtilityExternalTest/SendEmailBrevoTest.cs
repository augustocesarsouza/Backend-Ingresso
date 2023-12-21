using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;
using Ingresso.Infra.Data.UtilityExternal;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using Moq;
using sib_api_v3_sdk.Model;
using Xunit;

namespace Ingresso.Infra.Data.UtilityExternalTest
{
    public class SendEmailBrevoTest
    {
        private readonly SendEmailBrevo _sendEmailBrevo;
        public Mock<ITransactionalEmailApiUti> _transactionalEmailApiUti { get; } = new();

        public SendEmailBrevoTest()
        {
            _sendEmailBrevo = new SendEmailBrevo(_transactionalEmailApiUti.Object);
        }

        [Fact]
        public void Should_Send_Email_Without_Erro()
        {
            var guidId = Guid.NewGuid();
            var user = new User(guidId, "lucas", "lucasdaniel545@gmail.comm");
            var url = "dvdsv";

            _transactionalEmailApiUti.Setup(
                tran => tran.SendTransacEmailWrapper(It.IsAny<SendSmtpEmail>())).Returns(InfoErrors.Ok("tudo ok"));

            var result = _sendEmailBrevo.SendEmail(user, url);

            Assert.True(result.IsSucess);
        }

        [Fact]
        public void Should_Send_Email_With_Erro()
        {
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "lucas", "lucasdaniel545@gmail.comm");
            var url = "dvdsv";

            _transactionalEmailApiUti.Setup(
                tran => tran.SendTransacEmailWrapper(It.IsAny<SendSmtpEmail>())).Returns(InfoErrors.Fail("tudo ok"));

            var result = _sendEmailBrevo.SendEmail(user, url);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public void Should_Throw_Error_User_Name_Invalid()
        {
            var guidId = Guid.NewGuid();

            var user = new User(guidId, string.Empty, "lucasdaniel545@gmail.comm");
            var url = "dvdsv";

            var result = _sendEmailBrevo.SendEmail(user, url);

            Assert.False(result.IsSucess);
        }

        [Fact]
        public void Should_Throw_Error_User_Email_Invalid()
        {
            var guidId = Guid.NewGuid();

            var user = new User(guidId, "lucas", string.Empty);
            var url = "dvdsv";

            var result = _sendEmailBrevo.SendEmail(user, url);

            Assert.False(result.IsSucess);
        }
    }
}
