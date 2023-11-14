using Ingresso.Domain.Authentication;

namespace Ingresso.Api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public string? Email { get; private set; }
        public string? Cpf { get; private set; }
        public string? Password { get; private set; }
        public bool IsValid { get; private set; }

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            InitializeFromHttpContext(httpContext);
        }

        private void InitializeFromHttpContext(IHttpContextAccessor httpContext)
        {
            var claims = httpContext.HttpContext?.User.Claims;

            if (claims != null && claims.Any(x => x.Type == "Email"))
            {
                Email = claims.First(x => x.Type == "Email").Value;

                var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                IsValid = dt.DateTime > DateTime.UtcNow;
            }

            if (claims != null && claims.Any(x => x.Type == "Cpf"))
            {
                Cpf = claims.First(x => x.Type == "Cpf").Value;

                var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                IsValid = dt.DateTime > DateTime.UtcNow;
            }

            if (claims != null && claims.Any(x => x.Type == "Password"))
            {
                Password = claims.First(x => x.Type == "Password").Value;
            }
        }

        public void SetEmail(string? email)
        {
            Email = email;
        }

        public void SetCpf(string? cpf)
        {
            Cpf = cpf;
        }

        public void SetPassword(string? password)
        {
            Password = password;
        }

        public void SetIsValid(bool isValid)
        {
            IsValid = isValid;
        }
    }
}