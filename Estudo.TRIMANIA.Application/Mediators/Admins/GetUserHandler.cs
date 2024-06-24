using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Admins;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;

namespace Estudo.TRIMANIA.Application.Mediators.Admins
{
    public class GetUserHandler : BaseHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<GetUserRequest> validator) : base(validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        protected async override Task<GetUserResponse> HandleBase(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailOrNameOrLogin(request.Filter);

            var response = _mapper.Map<GetUserResponse>(user);

            return response;
        }
    }
}
