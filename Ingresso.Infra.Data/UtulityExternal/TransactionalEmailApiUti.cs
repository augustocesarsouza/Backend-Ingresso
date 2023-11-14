using Ingresso.Domain.InfoErrors;
using Ingresso.Infra.Data.UtulityExternal.Interface;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;

namespace Ingresso.Infra.Data.UtulityExternal
{
    public class TransactionalEmailApiUti : ITransactionalEmailApiUti
    {
        private readonly ITransactionalEmailsApi _transactionalEmailApi;

        public TransactionalEmailApiUti(ITransactionalEmailsApi transactionalEmailApi)
        {
            _transactionalEmailApi = transactionalEmailApi;
        }

        public InfoErrors SendTransacEmailWrapper(SendSmtpEmail sendSmtpEmail)
        {
            try
            {
                var createResult = _transactionalEmailApi.SendTransacEmail(sendSmtpEmail);
                if (createResult == null)
                    return InfoErrors.Fail("erro return null SendTransacEmail");

                return InfoErrors.Ok(createResult);
            }
            catch(Exception ex)
            {
                return InfoErrors.Fail($"Erro exception, ${ex.Message}");
            }
        }
    }
}
