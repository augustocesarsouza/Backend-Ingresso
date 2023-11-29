using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;
using Ingresso.Infra.Data.UtulityExternal.Interface;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;

namespace Ingresso.Infra.Data.UtulityExternal
{
    public class SendEmailBrevo : ISendEmailBrevo
    {
        private readonly ITransactionalEmailApiUti _transactionalEmailApiUti;

        public SendEmailBrevo(ITransactionalEmailApiUti transactionalEmailApiUti)
        {
            _transactionalEmailApiUti = transactionalEmailApiUti;
        }

        public InfoErrors SendEmail(User user, string url)
        {
            try
            {
                var keyApi = "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";

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "augusto";
                string SenderEmail = "augustocesarsantana90@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("Erro name ou email invalido");

                string ToName = user.Name;
                string ToEmail = user.Email;
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Clique no token disponivel: " + url;
                string Subject = "Seu token de confirmação";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }

        public InfoErrors SendCode(User user, int codeRandon)
        {
            try
            {
                var keyApi = "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";

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "augusto";
                string SenderEmail = "augustocesarsantana53@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("Erro name ou email invalido");

                string ToName = user.Name ?? "";
                string ToEmail = user.Email ?? "";
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Seu numero de Confirmação: " + codeRandon.ToString();
                string Subject = "SEU NUMERO ALEATORIO DE CONFIRMAÇÃO";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }
    }
}
