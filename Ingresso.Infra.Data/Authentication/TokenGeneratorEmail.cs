using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ingresso.Infra.Data.Authentication
{
    public class TokenGeneratorEmail : ITokenGeneratorEmail
    {
        private readonly IConfiguration _configuration;

        public TokenGeneratorEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InfoErrors<TokenOutValue> Generator(User user, ICollection<UserPermission> userPermissions, string? password)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(password))
                return InfoErrors.Fail(new TokenOutValue(), "Cpf or password null check");

            var permission = string.Join(",", userPermissions.Select(x => x.Permission?.PermissionName));
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Password", password),
                new Claim("userID", user.Id.ToString()),
                new Claim("Permissioes", permission),
            };

            var keySecret = _configuration["KeyJWT"];

            if (string.IsNullOrEmpty(keySecret) || keySecret.Length < 16)
                return InfoErrors.Fail(new TokenOutValue(), "error token related");

            var expires = DateTime.UtcNow.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims);
            
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            var tokenValue = new TokenOutValue();
            var sucessfullyCreatedToken = tokenValue.ValidateToken(token, expires);

            if (sucessfullyCreatedToken)
            {
                return InfoErrors.Ok(tokenValue);
            }
            else
            {
                return InfoErrors.Fail(new TokenOutValue(), "error when creating token");
            }
        }
    }
}
