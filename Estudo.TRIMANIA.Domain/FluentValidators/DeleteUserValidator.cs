using Estudo.TRIMANIA.Domain.Commands.Users;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            RuleFor(r => r.GetIdentification())
                .NotEmpty()
                .NotNull();
        }
    }
}
