using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        //[Authorize]
        [HttpGet("v1/region/by-name-city/{state}")]
        public async Task<IActionResult> GetIdByNameCity([FromRoute] string state)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _regionService.GetIdByNameCity(state);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
