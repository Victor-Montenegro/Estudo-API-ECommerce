using Estudo.TRIMANIA.Domain.Commands.Bases;
using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Users
{
    public class DeleteUserRequest : AuthorizeBaseRequest, IRequest<Unit>
    {
    }

}

