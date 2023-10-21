using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("v1/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDto userDto)
        {
            var result = await _userService.CreateAsync(userDto);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/user/login/{cpfOrEmail}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string cpfOrEmail, [FromRoute] string password)
        {
            var result = await _userService.Login(cpfOrEmail, password);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
