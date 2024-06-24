using Estudo.TRIMANIA.Domain.Commands.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estudo.TRIMANIA.Api.Controllers
{
    //[Authorize]
    public class CartController : ApiControllerBase
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CreateShoppingCartRequest request)
        {
            request.SetIdentification(Guid.Parse("4D96F11D-DB07-415B-80B2-C85F4CFD26AF"));

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> CartUpdate([FromBody] UpdateShoppingCartRequest request)
        {
            request.SetIdentification(Guid.Parse("4D96F11D-DB07-415B-80B2-C85F4CFD26AF"));

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
