using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Admins
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public string? Filter { get; set; }
    }
}
