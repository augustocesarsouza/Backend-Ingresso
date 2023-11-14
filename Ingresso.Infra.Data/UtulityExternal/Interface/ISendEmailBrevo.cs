using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;

namespace Ingresso.Infra.Data.UtulityExternal.Interface
{
    public interface ISendEmailBrevo
    {
        public InfoErrors SendEmail(User user, string url);
    }
}
