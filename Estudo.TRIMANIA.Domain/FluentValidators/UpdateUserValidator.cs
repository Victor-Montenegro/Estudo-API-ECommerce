using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Extensions;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(signUp => signUp.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(signUp => signUp.Email)
                .Must(email => email.IsEmailValid());

            RuleFor(signUp => signUp.Birthday)
                .NotNull();

            RuleFor(signUp => signUp.Address)
                .NotNull().SetValidator(new AddressValidator());
        }
    }
}
