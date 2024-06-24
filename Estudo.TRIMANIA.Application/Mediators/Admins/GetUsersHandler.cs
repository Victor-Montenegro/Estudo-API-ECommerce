using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Admins;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;

namespace Estudo.TRIMANIA.Application.Mediators.Admins
{
    internal class GetUsersHandler : BaseHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersHandler(IMapper mapper, IValidator<GetUsersRequest> validator, IUnitOfWork unitOfWork) : base(validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        protected async override Task<GetUsersResponse> HandleBase(GetUsersRequest request, CancellationToken cancellationToken)
        {
           var response = new GetUsersResponse();

            IEnumerable<User> users = await _unitOfWork.UserRepository.GetUsersPagination(request.Page, request.PageSize);

            response.Page = request.Page;
            response.PageSize = request.PageSize;
            response.Users = _mapper.Map<IEnumerable<GetUserResponse>>(users);

            return response;
        }
    }
}
