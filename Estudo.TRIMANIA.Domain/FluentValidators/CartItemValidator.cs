using Estudo.TRIMANIA.Domain.Commands.Carts;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    internal class CartItemValidator : AbstractValidator<CartItemRequest>
    {
        public CartItemValidator()
        {
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(1);

            RuleFor(c => c.ProductId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
