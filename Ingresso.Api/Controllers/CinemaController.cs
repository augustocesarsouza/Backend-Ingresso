using Ingresso.Api.ControllersInterface;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly ICurrentUser _currentUser;
        private readonly IBaseController _baseController;

        public CinemaController(ICinemaService cinemaService, ICurrentUser currentUser, IBaseController baseController)
        {
            _cinemaService = cinemaService;
            _currentUser = currentUser;
            _baseController = baseController;
        }

        //[Authorize]
        [HttpPost("v1/cinema/create")]
        public async Task<IActionResult> Create([FromBody] CinemaDTO cinemaDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _cinemaService.Create(cinemaDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
