using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;

namespace Ingresso.Infra.Data.SendEmailUser
{
    public interface ISendEmailUser
    {
        public Task<InfoErrors> SendEmail(User user); 
    }
}
