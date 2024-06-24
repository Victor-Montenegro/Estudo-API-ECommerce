using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;
using MediatR;

namespace Estudo.TRIMANIA.Application.Mediators.Users
{
    public class UpdateUserHandler : BaseHandler<UpdateUserRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork,IMapper mapper, IValidator<UpdateUserRequest> validator) : base(validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Unit> HandleBase(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdentification(request.GetIdentification());

            user.SetAddress(_mapper.Map<Address>(request.Address));
            user.InformationUpdate(request.Name, request.Birthday, request.Email);

            await _unitOfWork.UserRepository.Update(user, false);

            return Unit.Value;
        }
    }
}
