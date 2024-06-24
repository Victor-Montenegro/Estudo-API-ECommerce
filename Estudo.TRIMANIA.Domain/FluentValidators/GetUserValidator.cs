using Estudo.TRIMANIA.Domain.Commands.Admins;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class GetUserValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserValidator()
        {
            RuleFor(r => r.Filter).NotEmpty().NotNull();
        }
    }
}
