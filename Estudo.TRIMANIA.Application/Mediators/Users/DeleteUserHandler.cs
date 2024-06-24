using Estudo.TRIMANIA.Domain.Commands.Users;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Domain.Exceptions;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;
using MediatR;

namespace Estudo.TRIMANIA.Application.Mediators.Users
{
    public class DeleteUserHandler : BaseHandler<DeleteUserRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUnitOfWork unitOfWork, IValidator<DeleteUserRequest> validator) : base(validator)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Unit> HandleBase(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdentification(request.GetIdentification());

            if (user is null)
                new ServiceException("Usuário não existe.");

            await UserIsThereOrder(user);

            await _unitOfWork.UserRepository.Delete(user, false);

            return Unit.Value;
        }

        private async Task UserIsThereOrder(User? user)
        {
            var isThereOrder = await _unitOfWork.OrderRepository.UserAreThereOrders(user.Id);

            if (isThereOrder)
                throw new ServiceException("Não é possivel excluir usuário com pedidos");
        }
    }
}
