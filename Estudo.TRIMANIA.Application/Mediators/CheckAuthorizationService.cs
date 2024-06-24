using Estudo.TRIMANIA.Domain.Commands;
using FluentValidation;
using MediatR;

namespace Estudo.TRIMANIA.Application.Mediators
{
    public class CheckAuthorizationService : BaseHandler<CheckAuthorizationRequest, Unit>
    {
        public CheckAuthorizationService(IValidator<CheckAuthorizationRequest> validator) : base(validator)
        {
        }

        protected async override Task<Unit> HandleBase(CheckAuthorizationRequest request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
