using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class CinemaMovieController : ControllerBase
    {
        private readonly ICinemaMovieService _cinemaMovieService;

        public CinemaMovieController(ICinemaMovieService cinemaMovieService)
        {
            _cinemaMovieService = cinemaMovieService;
        }

        //[Authorize]
        [HttpGet("v1/cinemaMovie/getAll/{region}/{movieId}")]
        public async Task<IActionResult> GetByRegionCinemaIdAndMovieId([FromRoute] string region, [FromRoute] string movieId)
        {
            //var userAuth = Validator(_currentUser);
            //if (userAuth == null)
            //    return Forbidden();

            var result = await _cinemaMovieService.GetByRegionCinemaIdAndMovieId(region, Guid.Parse(movieId));
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/cinemaMovie/create")]
        public async Task<IActionResult> Create([FromBody] CinemaMovieDTO cinemaMovieDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _cinemaMovieService.Create(cinemaMovieDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
