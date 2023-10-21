namespace Ingresso.Domain.Entities
{
    public class UserPermission
    {
        public int Id { get; private set; }

        public int? UserId { get; private set; }
        public User? User { get; private set; }
        public int? PermissionId { get; private set; }
        public Permission? Permission { get; private set; }

        public UserPermission()
        {
        }

        public UserPermission(int id, int? userId, Permission permission)
        {
            Validator(id, userId, permission);
        }

        public void Validator(int id, int? userId, Permission? permission)
        {
            DomainValidationException.When(id <= 0, "Id do Usuario Should be informed");
            DomainValidationException.When(userId <= 0, "userId do Usuario Should be informed");
            DomainValidationException.When(permission == null, "permission cannot be null");

            Id = id;
            UserId = userId;
            Permission = permission;
        }
    }
}
