using Ingresso.Infra.Data.UtulityExternal.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace Ingresso.Infra.Data.SendEmailUser
{
    public class SendEmailUserConfiguration
    {
        public Mock<ICacheRedisUti> CacheRedisUti { get; }
        public Mock<ISendEmailBrevo> SendEmailBrevo { get; }

        public SendEmailUserConfiguration()
        {
            CacheRedisUti = new();
            SendEmailBrevo = new();
        }
    }
}
