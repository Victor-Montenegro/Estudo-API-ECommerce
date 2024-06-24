using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Users
{
    public class SignUpRequest : IRequest<Unit>
    {
        public string? CPF { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime Birthday { get; set; }
        public AddressRequest? Address { get; set; }
    }

    public class AddressRequest
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
    }
}
