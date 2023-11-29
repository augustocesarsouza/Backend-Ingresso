using Ingresso.Application.CodeRandomUser;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly ICurrentUser _currentUser;

        public UserController(
            ICurrentUser currentUser, IUserManagementService userManagementService,
            IUserAuthenticationService userAuthenticationService, IUserConfirmationService userConfirmationService
            )
        {
            _currentUser = currentUser;
            _userManagementService = userManagementService;
            _userAuthenticationService = userAuthenticationService;
            _userConfirmationService = userConfirmationService;
        }

        [HttpGet]
        [Route("v1/user/login/{cpfOrEmail}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string cpfOrEmail, [FromRoute] string password)
        {
            var result = await _userAuthenticationService.Login(cpfOrEmail, password);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/user/getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var userAuth = Validator(_currentUser);
            if (userAuth == null)
                return Forbidden();

            var results = await _userManagementService.GetUsers();

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpGet("v1/user/confirm-token/{token}")]
        public async Task<IActionResult> GetConfirmToken(string token)
        {
            var results = await _userConfirmationService.GetConfirmToken(token);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpGet("v1/user/verific/{code}/{guidId}")]
        public IActionResult Verfic([FromRoute] int code, [FromRoute] string guidId)
        {
            var results = _userAuthenticationService.Verfic(code, guidId);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpPost("v1/user/resend-code")]
        public IActionResult ResendCode([FromBody] UserDto user)
        {
            var results = _userAuthenticationService.ResendCode(user);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }


        [HttpPost("v1/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDto userDto)
        {
            var result = await _userManagementService.CreateAsync(userDto);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("v1/user/update-user/{password}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDto userDto, string password)
        {
            var userAuth = Validator(_currentUser);
            if (userAuth == null)
                return Forbidden();

            var result = await _userManagementService.UpdateUser(userDto, password);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPut("v1/user/update-user-password")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserPasswordChangeDTO userPasswordChangeDTO) 
        {
            //var userAuth = Validator(_currentUser);
            //if (userAuth == null)
            //    return Forbidden();

            var result = await _userManagementService.UpdateUserPassword(userPasswordChangeDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
