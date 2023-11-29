using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class AdditionalInfoUserController : BaseController
    {
        private readonly IAdditionalInfoUserService _additionalInfoUserService;
        private readonly ICurrentUser _currentUser;

        public AdditionalInfoUserController(IAdditionalInfoUserService additionalInfoUserService, ICurrentUser currentUser)
        {
            _additionalInfoUserService = additionalInfoUserService;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/info-user/{idGuid}")]
        public async Task<IActionResult> GetInfoUser([FromRoute] string idGuid)
        {
            var userAuth = Validator(_currentUser);

            if (userAuth == null)
                return Forbidden();

            var result = await _additionalInfoUserService.GetInfoUser(idGuid);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("v1/additional/update-info-user/{password}")]
        public async Task<IActionResult> UpdateAsync([FromBody] AdditionalInfoUserDTO additionalInfoUserDTO, string password)
        {
            var userAuth = Validator(_currentUser);

            if (userAuth == null)
                return Forbidden();

            var result = await _additionalInfoUserService.UpdateAsync(additionalInfoUserDTO, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
