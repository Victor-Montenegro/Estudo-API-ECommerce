using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estudo.TRIMANIA.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
