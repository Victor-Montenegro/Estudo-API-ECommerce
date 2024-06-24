namespace Estudo.TRIMANIA.Domain.Commands.Bases
{
    public class AuthorizeBaseRequest
    {
        private Guid _Identification;

        public void SetIdentification(Guid identification)
        {
            _Identification = identification;
        }

        public Guid GetIdentification()
            => _Identification;
    }
}
