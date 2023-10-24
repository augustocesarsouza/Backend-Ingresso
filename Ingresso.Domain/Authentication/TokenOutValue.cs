namespace Ingresso.Domain.Authentication
{
    public class TokenOutValue
    {
        public string? Acess_Token { get; private set; }
        public DateTime Expirations { get; private set; }

        public TokenOutValue()
        {
        }

        public bool ValidateToken(string? acess_Token, DateTime expirations)
        {
            if (string.IsNullOrEmpty(acess_Token))
            {
                return false;
            }

            Acess_Token = acess_Token;
            Expirations = expirations;
            return true;
        }
    }
}
