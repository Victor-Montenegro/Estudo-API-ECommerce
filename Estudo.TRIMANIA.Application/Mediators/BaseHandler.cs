using FluentValidation;
using MediatR;

namespace Estudo.TRIMANIA.Application.Mediators
{
    public abstract class BaseHandler<TReq, TResp> : IRequestHandler<TReq, TResp>
        where TReq : IRequest<TResp>
    {
        private readonly IValidator<TReq> _validator;

        protected BaseHandler(IValidator<TReq> validator)
        {
            _validator = validator;
        }

        public Task<TResp> Handle(TReq request, CancellationToken cancellationToken)
        {
            FluentValidation(request);

            var response = HandleBase(request, cancellationToken);

            return response;
        }

        private void FluentValidation(TReq request)
        {
            _validator.ValidateAndThrow(request);
        }

        protected abstract Task<TResp> HandleBase(TReq request, CancellationToken cancellationToken);
    }
}
