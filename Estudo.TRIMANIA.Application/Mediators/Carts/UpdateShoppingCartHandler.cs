using AutoMapper;
using Estudo.TRIMANIA.Application.Bridges;
using Estudo.TRIMANIA.Domain.Commands.Carts;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Domain.Enums;
using Estudo.TRIMANIA.Domain.Exceptions;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using FluentValidation;
using System.Net;

namespace Estudo.TRIMANIA.Application.Mediators.Carts
{
    internal class UpdateShoppingCartHandler : BaseHandler<UpdateShoppingCartRequest, CreateShoppingCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockBridge _stockBridge;

        public UpdateShoppingCartHandler(IMapper mapper, IUnitOfWork unitOfWork, IStockBridge stockBridge, IValidator<UpdateShoppingCartRequest> validator) : base(validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _stockBridge = stockBridge;
        }

        protected async override Task<CreateShoppingCartResponse> HandleBase(UpdateShoppingCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var user = await _unitOfWork.UserRepository.GetUserByIdentification(request.GetIdentification());

                if (user is null)
                    throw new ServiceException("Usuário não encontrado", HttpStatusCode.BadRequest);

                var order = await _unitOfWork.OrderRepository.GetOrderByUserIdAndStatus(user.Id, EOrderStatus.Progress);

                if (order is null)
                    throw new ServiceException("Pedido não encontrado", HttpStatusCode.BadRequest);

                var orderItems = _mapper.Map<List<OrderItem>>(request.Items);

                await _stockBridge.SetProductPriceInOrderItem(orderItems);

                await descartarItemsAnteriores(order);

                order.ItemsUpdate(orderItems);

                await _unitOfWork.OrderRepository.Update(order);

                await _unitOfWork.Commit();

                var response = _mapper.Map<CreateShoppingCartResponse>(order);

                return response;
            }
            catch (Exception)
            {
                await _unitOfWork.RollBack();
                throw;
            }
        }

        private async Task descartarItemsAnteriores(Order order)
        {
            await _unitOfWork.OrderRepository.DeleteItemsBatch(order);
        }
    }
}
