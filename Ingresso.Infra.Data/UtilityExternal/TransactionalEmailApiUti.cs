using Ingresso.Domain.InfoErrors;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;

namespace Ingresso.Infra.Data.UtilityExternal
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
                    return InfoErrors.Fail("erro not completed");

                return InfoErrors.Ok(createResult);
            }
            catch(Exception ex)
            {
                return InfoErrors.Fail($"Erro exception, ${ex.Message}");
            }
        }
    }
}
