using Ingresso.Application.DTOs;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public UserAuthDTO? Validator(ICurrentUser? currentUser)
        {
            // password talvez não possa ser menor que 8 talvez

            if (currentUser == null || string.IsNullOrEmpty(currentUser.Password))
                return null;

            if (string.IsNullOrEmpty(currentUser.Email) && string.IsNullOrEmpty(currentUser.Cpf))
                return null;

            if(!currentUser.IsValid)
                return null;

            if (!string.IsNullOrEmpty(currentUser.Email))
            {
                return new UserAuthDTO { Email = currentUser.Email, Password = currentUser.Password };
            }
            else if (!string.IsNullOrEmpty(currentUser.Cpf))
            {
                return new UserAuthDTO { Cpf = currentUser.Cpf, Password = currentUser.Password };
            }

            return null;
        }

        [NonAction]
        public IActionResult Forbidden()
        {
            var obj = new
            {
                code = "acesso_negado",
                message = "Usuario não contem as devidas informações necessarias para acesso"
            };

            return new ObjectResult(obj) { StatusCode = 403 };
        }
    }
}
