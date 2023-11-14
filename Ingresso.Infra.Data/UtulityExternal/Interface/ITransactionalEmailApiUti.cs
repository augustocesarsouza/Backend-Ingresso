using Ingresso.Domain.InfoErrors;
using sib_api_v3_sdk.Model;

namespace Ingresso.Infra.Data.UtulityExternal.Interface
{
    public interface ITransactionalEmailApiUti
    {
        public InfoErrors SendTransacEmailWrapper(SendSmtpEmail sendSmtpEmail);
    }
}
