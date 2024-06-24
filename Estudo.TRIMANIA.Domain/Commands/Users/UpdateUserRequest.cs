using Estudo.TRIMANIA.Domain.Commands.Bases;
using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Users
{
    public class UpdateUserRequest : AuthorizeBaseRequest, IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public AddressRequest? Address { get; set; }
    }
}
