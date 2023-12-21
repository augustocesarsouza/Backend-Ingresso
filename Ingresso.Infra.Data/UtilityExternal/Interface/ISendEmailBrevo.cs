using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;

namespace Ingresso.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailBrevo
    {
        public InfoErrors SendEmail(User user, string url);
        public InfoErrors SendCode(User user, int codeRandon);
    }
}
