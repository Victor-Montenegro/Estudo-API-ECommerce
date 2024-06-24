using MediatR;
using System.Security.Claims;

namespace Estudo.TRIMANIA.Domain.Commands
{
    public class CheckAuthorizationRequest : IRequest<Unit>
    {
        public string? Endpoint { get; set; }
        public ClaimsPrincipal? Claims { get; set; }
    }
}
