namespace Estudo.TRIMANIA.Domain.Commands.Admins
{
    public class GetUserResponse
    {
        public string? CPF { get; private set; }
        public string? Name { get; private set; }
        public string? Login { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public DateTime Birthday { get; private set; }
        public Guid Identification { get; private set; }
    }
}