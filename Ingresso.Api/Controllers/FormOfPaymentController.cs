using Ingresso.Api.ControllersInterface;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Ingresso.Api.Controllers
{
    public class FormOfPaymentController : ControllerBase
    {
        private readonly IFormOfPaymentService _formOfPaymentService;
        private readonly ICurrentUser _currentUser;
        private readonly IBaseController _baseController;

        public FormOfPaymentController(IFormOfPaymentService formOfPaymentService, ICurrentUser currentUser, IBaseController baseController)
        {
            _formOfPaymentService = formOfPaymentService;
            _currentUser = currentUser;
            _baseController = baseController;
        }

        //[Authorize]
        [HttpGet("v1/formofpayment/get-form/{movieid}")]
        public async Task<IActionResult> GetMovieIDInfo([FromRoute] Guid movieid)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _formOfPaymentService.GetMovieIDInfo(movieid);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        //[Authorize]
        [HttpPost("v1/formofpayment/create")]
        public async Task<IActionResult> Create([FromBody] FormOfPaymentDTO formOfPaymentDTO)
        {
            //var userAuth = Validator(_currentUser);

            //if (userAuth == null)
            //    return Forbidden();

            var result = await _formOfPaymentService.Create(formOfPaymentDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
