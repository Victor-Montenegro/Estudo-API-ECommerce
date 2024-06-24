using Estudo.TRIMANIA.Domain.Commands.Admins;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estudo.TRIMANIA.Api.Controllers
{
    //[Authorize]
    public class AdminController : ApiControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [Route("getusers")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("getuser")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
