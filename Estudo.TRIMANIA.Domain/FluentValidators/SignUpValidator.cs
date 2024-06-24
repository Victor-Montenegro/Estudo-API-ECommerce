using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Extensions;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(signUp => signUp.CPF)
                .Must(cpf => cpf.FormatCpf().IsCpfValid());

            RuleFor(signUp => signUp.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(signUp => signUp.Login)
                .NotEmpty()
                .NotNull();

            RuleFor(signUp => signUp.Email)
                .Must(email => email.IsEmailValid());

            RuleFor(signUp => signUp.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(signUp => signUp.Birthday)
                .NotNull();

            RuleFor(signUp => signUp.Address)
                .NotNull().SetValidator(new AddressValidator());
        }
    }
}
