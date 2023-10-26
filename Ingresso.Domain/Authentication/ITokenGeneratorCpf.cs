using Ingresso.Domain.Entities;
using Ingresso.Domain.InfoErrors;

namespace Ingresso.Domain.Authentication
{
    public interface ITokenGeneratorCpf
    {
        InfoErrors<TokenOutValue> Generator(User user, ICollection<UserPermission> userPermissions, string? password);
    }
}
