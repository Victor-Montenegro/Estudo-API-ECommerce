namespace Estudo.TRIMANIA.Domain.Commands.Admins
{
    public class GetUsersResponse
    {
        public IEnumerable<GetUserResponse>? Users { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
