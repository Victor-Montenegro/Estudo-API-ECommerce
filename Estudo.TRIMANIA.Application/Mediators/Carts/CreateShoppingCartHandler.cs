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
    internal class CreateShoppingCartHandler : BaseHandler<CreateShoppingCartRequest, CreateShoppingCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockBridge _stockBridge;

        public CreateShoppingCartHandler(IStockBridge stockBridge, IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateShoppingCartRequest> validator) : base(validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _stockBridge = stockBridge;
        }

        protected override async Task<CreateShoppingCartResponse> HandleBase(CreateShoppingCartRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdentification(request.GetIdentification());

            if (user is null)
                throw new ServiceException("Usuário não encontrado", HttpStatusCode.BadRequest);

            await IsThereOrderInProgress(user);

            var orderItems = _mapper.Map<List<OrderItem>>(request.Items);

            await _stockBridge.SetProductPriceInOrderItem(orderItems);

            var order = new Order();

            order.InitNewOrder(user.Id, orderItems);

            await _unitOfWork.OrderRepository.Insert(order, false);

            var response = _mapper.Map<CreateShoppingCartResponse>(order);

            return response;
        }

        private async Task IsThereOrderInProgress(User user)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByUserIdAndStatus(user.Id, EOrderStatus.Progress);

            if (order is not null)
                throw new ServiceException($"Antes de criar um novo pedido deve cancelar ou concluir o pedido N° {order.Id}", HttpStatusCode.BadRequest);
        }
    }
}
