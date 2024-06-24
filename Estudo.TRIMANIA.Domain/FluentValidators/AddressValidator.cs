using Estudo.TRIMANIA.Domain.Commands.Users;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class AddressValidator : AbstractValidator<AddressRequest>
    {
        public AddressValidator()
        {
            RuleFor(address => address.City).NotNull().NotEmpty();
            RuleFor(address => address.State).NotNull().NotEmpty();
            RuleFor(address => address.Number).NotNull().NotEmpty();
            RuleFor(address => address.Street).NotNull().NotEmpty();
            RuleFor(address => address.Neighborhood).NotNull().NotEmpty();
        }
    }
}
