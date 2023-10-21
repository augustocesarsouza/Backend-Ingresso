using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Authentication
{
    public interface ITokenGenerator
    {
        TokenOutValue Generator(User user, ICollection<UserPermission> userPermissions);
    }
}
