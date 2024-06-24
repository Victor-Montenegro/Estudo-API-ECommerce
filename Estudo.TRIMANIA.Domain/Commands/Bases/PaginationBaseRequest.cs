namespace Estudo.TRIMANIA.Domain.Commands.Bases
{
    public abstract class PaginationBaseRequest
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
