using Ingresso.Api.ControllersInterface;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUser _currentUser;
        private readonly IBaseController _baseController;

        public MovieController(IMovieService movieService, ICurrentUser currentUser, IBaseController baseController)
        {
            _movieService = movieService;
            _currentUser = currentUser;
            _baseController = baseController;
        }

        //[Authorize]
        [HttpGet("v1/movie/get-all-region/{region}")]
        public async Task<IActionResult> GetAllMovieByRegionId([FromRoute] string region)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.GetAllMovieByRegionId(region);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpGet("v1/movie/info/{idGuid}")]
        public async Task<IActionResult> GetInfoMoviesById([FromRoute] string idGuid)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.GetInfoMoviesById(Guid.Parse(idGuid));
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpGet("v1/movie/get-status-movie/{statusMovie}")]
        public async Task<IActionResult> GetStatusMovie([FromRoute] string statusMovie)
        {
            //var userAuth = _baseController.Validator(_currentUser);

            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _movieService.GetStatusMovie(statusMovie);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/movie/create")]
        public async Task<IActionResult> Create([FromBody] MovieDTO movieDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.Create(movieDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpDelete("v1/movie/delete/{idMovie}")]
        public async Task<IActionResult> Delete([FromRoute] string idMovie)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.DeleteMovie(Guid.Parse(idMovie));
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPut("v1/movie/update")]
        public async Task<IActionResult> Update([FromBody] MovieDTO movieDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.UpdateMovie(movieDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPut("v1/movie/update-imgbackground")]
        public async Task<IActionResult> UpdateImgBackground([FromBody] MovieDTO movieDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _movieService.UpdateMovieImgBackground(movieDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
