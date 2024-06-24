using Estudo.TRIMANIA.Domain.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Estudo.TRIMANIA.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] string asd)
        {
            var response = await _mediator.Send(asd);

            return Ok(response);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            await _mediator.Send(request);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            request.SetIdentification(Guid.NewGuid());

            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserRequest request)
        {
            request.SetIdentification(Guid.NewGuid());

            await _mediator.Send(request);

            return Ok();
        }
    }
}
