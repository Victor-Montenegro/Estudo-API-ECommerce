using Estudo.TRIMANIA.Domain.Commands.Bases;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public abstract class PaginationBaseValidator<TRequest> : AbstractValidator<TRequest>
        where TRequest : PaginationBaseRequest
    {
        public PaginationBaseValidator()
        {
            RuleFor(r => r.Page)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.PageSize)
                .GreaterThan(0);
        }
    }
}
