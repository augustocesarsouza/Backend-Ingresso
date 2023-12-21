using Ingresso.Application.DTOs;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.ControllersInterface
{
    public interface IBaseController
    {
        public UserAuthDTO? Validator(ICurrentUser? currentUser);
        public IActionResult Forbidden();
    }
}
