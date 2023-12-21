using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService _theatreService;

        public TheatreController(ITheatreService theatreService)
        {
            _theatreService = theatreService;
        }

        [HttpGet("v1/theatre/get-all-region/{region}")]
        public async Task<IActionResult> GetAllTheatreByRegionId([FromRoute] string region)
        {
            var result = await _theatreService.GetAllTheatreByRegionId(region);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/theatre/create")]
        public async Task<IActionResult> Create([FromBody] TheatreDTO theatreDTO)
        {
            var result = await _theatreService.Create(theatreDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpDelete("v1/theatre/delete/{idTheatre}")]
        public async Task<IActionResult> Delete([FromRoute] string idTheatre)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _theatreService.DeleteTheatre(Guid.Parse(idTheatre));
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPut("v1/theatre/update")]
        public async Task<IActionResult> Update([FromBody] TheatreDTO theatreDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _theatreService.UpdateMovie(theatreDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
