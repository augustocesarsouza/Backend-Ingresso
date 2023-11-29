namespace Ingresso.Application.DTOs
{
    public class UserPasswordChangeDTO
    {
        public string? PasswordCurrent { get; set; }
        public string? NewPassword { get; set; }
        public Guid? IdGuid { get; set; }
    }
}
