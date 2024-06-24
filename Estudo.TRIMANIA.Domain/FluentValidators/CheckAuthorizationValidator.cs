using Estudo.TRIMANIA.Domain.Commands;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class CheckAuthorizationValidator : AbstractValidator<CheckAuthorizationRequest>
    {
        public CheckAuthorizationValidator()
        {
            RuleFor(x => x.Endpoint)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Claims)
                .NotEmpty()
                .NotNull();
        }
    }
}
