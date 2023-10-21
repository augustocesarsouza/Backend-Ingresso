using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ingresso.Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public TokenOutValue Generator(User user, ICollection<UserPermission> userPermissions)
        {
            var permission = string.Join(",", userPermissions.Select(x => x.Permission?.PermissionName));
            var claims = new List<Claim>
            {

                new Claim("Name", user.Name ?? "name not found"),
                new Claim("Email", user.Email ?? "email not found"),
                new Claim("userID", user.Id.ToString()),
                new Claim("Permissioes", permission),
            };

            var expires = DateTime.Now.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GXN0jYSyPZYP3D3WULO8naEHQp9XRP347UvK5I"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new TokenOutValue(token, expires);
        }
    }
}
