namespace Ingresso.Domain.Authentication
{
    public class TokenOutValue
    {
        public string? Acess_Token { get; set; }
        public DateTime Expirations { get; set; }

        public TokenOutValue(string? acess_Token, DateTime expirations)
        {
            Acess_Token = acess_Token;
            Expirations = expirations;
        }
    }
}
