using Estudo.TRIMANIA.Domain.Commands.Bases;
using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Admins
{
    public class GetUsersRequest : PaginationBaseRequest, IRequest<GetUsersResponse>
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
    }
}
