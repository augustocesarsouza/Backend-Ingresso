using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ingresso.Domain.InfoErrors;
using Ingresso.Domain.Entities;
using Ingresso.Infra.Data.UtulityExternal.Interface;

namespace Ingresso.Infra.Data.SendEmailUser
{
    public class SendEmailUser : ISendEmailUser
    {
        private readonly ICacheRedisUti _distributedCache;
        private readonly ISendEmailBrevo _sendEmailBrevo;

        public SendEmailUser(ICacheRedisUti distributedCache, ISendEmailBrevo sendEmailBrevo)
        {
            _distributedCache = distributedCache;
            _sendEmailBrevo = sendEmailBrevo;
        }

        public async Task<InfoErrors> SendEmail(User user)
        {
            try
            {
                var claims = new List<Claim>
                    {
                    new Claim("id", user.Id.ToString()),
                    };

                var expires = DateTime.UtcNow.AddMinutes(10);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("seilakey123seilakey"));
                var tokenValidate = new JwtSecurityToken(
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                    expires: expires,
                    claims: claims);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenValidate);

                var chaveKey = "TokenString" + user.Id.ToString();
                var cache = await _distributedCache.GetStringAsyncWrapper(chaveKey);

                if (string.IsNullOrEmpty(cache))
                {
                    var cacheEntryOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    };

                    await _distributedCache.SetStringAsyncWrapper(chaveKey, JsonConvert.SerializeObject(tokenString), cacheEntryOptions);
                }

                var url = $"http://localhost:5700/minha-conta/confirmacao-de-email?token={tokenString}";

                var resultSend = _sendEmailBrevo.SendEmail(user, url);

                if (!resultSend.IsSucess)
                    return InfoErrors.Fail(resultSend.Message ?? "error envio do email");

                return InfoErrors.Ok("tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Falha ao enviar email, ERRO: ${ex.Message}");
            }
        }

        public InfoErrors SendCodeRandom(User user, int code)
        {
            try
            {
                var resultSend = _sendEmailBrevo.SendCode(user, code);

                if (!resultSend.IsSucess)
                    return InfoErrors.Fail(resultSend.Message ?? "error envio do email");

                return InfoErrors.Ok("tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Falha ao enviar email, ERRO: ${ex.Message}");
            }
        }
    }
}
