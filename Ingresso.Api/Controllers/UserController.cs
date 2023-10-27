using Ingresso.Api.Authentication;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserService userService, ICurrentUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route("v1/user/login/{cpfOrEmail}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string cpfOrEmail, [FromRoute] string password)
        {
            var result = await _userService.Login(cpfOrEmail, password);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/user/get")]
        public async Task<IActionResult> Get()
        {
            var userAuth = Validator(_currentUser);
            if (userAuth == null)
                return Forbidden();

            return Ok("ok");
        }


        [HttpPost("v1/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDto userDto)
        {
            var result = await _userService.CreateAsync(userDto);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
