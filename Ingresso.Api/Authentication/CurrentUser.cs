using Ingresso.Domain.Authentication;

namespace Ingresso.Api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Password { get; set; }
        public bool IsValid { get; set; }
       // private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser (IHttpContextAccessor httpContext)
        {
            var claims = httpContext?.HttpContext?.User.Claims; // ver isso

            if(claims != null && claims.Any(x => x.Type == "Email"))
            {
                Email = claims.First(x => x.Type == "Email").Value;

                var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                IsValid = dt.DateTime > DateTime.Now;
            }

            if (claims != null && claims.Any(x => x.Type == "Cpf"))
            {
                Cpf = claims.First(x => x.Type == "Cpf").Value;

                var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                IsValid = dt.DateTime > DateTime.Now;
            }

            if (claims != null && claims.Any(x => x.Type == "Password"))
            {
                Password = claims.First(x => x.Type == "Password").Value;
            }
        }

        //public void CreateCurrentUser()
        //{
        //    var claims = _contextAccessor?.HttpContext?.User.Claims; // ver isso

        //    if (claims.Any(x => x.Type == "Email"))
        //    {
        //        Email = claims.First(x => x.Type == "Email").Value;
        //    }

        //    if(claims.Any(x => x.Type == "Password"))
        //    {
        //        Password = claims.First(x => x.Type == "Password").Value;
        //    }

        //    if(claims.Any(x => x.Type == "exp"))
        //    {
        //        var time = long.Parse(claims.First(x => x.Type == "exp").Value);
        //        DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
        //        IsValid = dt.DateTime > DateTime.Now;
        //    }
        //}
    }
}
