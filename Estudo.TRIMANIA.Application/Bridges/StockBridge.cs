using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Domain.Exceptions;
using Estudo.TRIMANIA.Infrastructure.Repositories;
using System.Net;

namespace Estudo.TRIMANIA.Application.Bridges
{
    public class StockBridge : IStockBridge
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockBridge(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SetProductPriceInOrderItem(IEnumerable<OrderItem> orderItems, bool withCache = false)
        {
            foreach (var item in orderItems)
            {
                var product = await _unitOfWork.ProductRepository.GetProductById(item.product_id);

                if(product is null)
                    throw new ServiceException($"Produto não existe", HttpStatusCode.BadRequest);

                if (product.IsThereStock(item.Quantity))
                    throw new ServiceException($"Produto não está mais em estoque ou não existe", HttpStatusCode.BadRequest);

                item.SetPrice(product.Price);
            }
        }
    }

    public interface IStockBridge
    {
        Task SetProductPriceInOrderItem(IEnumerable<OrderItem> orderItems, bool withCache = false);
    }
}
