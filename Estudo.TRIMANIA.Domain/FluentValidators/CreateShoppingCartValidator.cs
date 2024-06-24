using Estudo.TRIMANIA.Domain.Commands.Carts;
using FluentValidation;

namespace Estudo.TRIMANIA.Domain.FluentValidators
{
    public class CreateShoppingCartValidator : AbstractValidator<CreateShoppingCartRequest>
    {
        public CreateShoppingCartValidator()
        {
            RuleFor(c => c.Items)
                .Must(items =>  items.Count == items.Select(s => s.ProductId).Distinct().Count())
                .WithMessage("Existem itens duplicados no seu carrinho");

            RuleForEach(c => c.Items)
                .NotEmpty()
                .SetValidator(new CartItemValidator());
        }
    }
}
