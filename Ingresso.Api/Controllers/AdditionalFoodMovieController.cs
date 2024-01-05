using Ingresso.Api.ControllersInterface;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class AdditionalFoodMovieController : ControllerBase
    {
        private readonly IAdditionalFoodMovieService _additionalFoodMovieService;
        private readonly ICurrentUser _currentUser;
        private readonly IBaseController _baseController;

        public AdditionalFoodMovieController(IAdditionalFoodMovieService additionalFoodMovieService, ICurrentUser currentUser, IBaseController baseController)
        {
            _additionalFoodMovieService = additionalFoodMovieService;
            _currentUser = currentUser;
            _baseController = baseController;
        }

        //[Authorize]
        [HttpGet("v1/additionalfoodmovie/getallfood/{movieId}")]
        public async Task<IActionResult> GetAllFoodMovie([FromRoute] Guid movieId)
        {
            //var userAuth = _baseController.Validator(_currentUser);

            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _additionalFoodMovieService.GetAllFoodMovie(movieId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/additionalfoodmovie/create")]
        public async Task<IActionResult> Create([FromBody] AdditionalFoodMovieDTO additionalFoodMovieDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);

            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _additionalFoodMovieService.Create(additionalFoodMovieDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
