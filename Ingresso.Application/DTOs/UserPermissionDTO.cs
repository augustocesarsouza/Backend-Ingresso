namespace Ingresso.Application.DTOs
{
    public class UserPermissionDTO
    {
        public int Id { get; set; }

        public Guid? UserId { get; set; }
        public UserDto? User { get; set; }
        public int? PermissionId { get; set; }
        public PermissionDTO? Permission { get; set; }
    }
}
