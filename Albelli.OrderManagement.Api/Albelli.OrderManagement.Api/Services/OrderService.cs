using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories.Interfaces;
using Albelli.OrderManagement.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductInfoRepository _productInfoRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductInfoRepository productInfoRepository)
        {
            _orderRepository = orderRepository;
            _productInfoRepository = productInfoRepository;
        }

        public async Task<OrderVM> GetByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<Entities.Order> AddAsync(IEnumerable<OrderLineVM> items)
        {
            var groupedItems = items.GroupBy(x => x.ProductType)
                .Select(x => new OrderLineVM
                {
                    ProductType = x.Key,
                    Quantity = x.Sum(y => y.Quantity)
                });

            var products = await _productInfoRepository.GetItemsByTypes(groupedItems.Select(x => x.ProductType));

            if (products.Count < groupedItems.Count())
            {
                var wrongTypes = groupedItems.Select(x => x.ProductType)
                    .Except(products.Select(x => x.ProductType))
                    .ToArray();

                throw new Exception($"Product with types: {string.Join(", ", wrongTypes)} not found.");
            }

            List<OrderLinePostModel> orderLines = products
                .Select(x => new OrderLinePostModel
                {
                    Id = x.Id,
                    WidthMm = x.WidthMm,
                    ProductType = x.ProductType,
                    Quantity = groupedItems
                        .Where(g => g.ProductType == x.ProductType)
                        .Select(g => g.Quantity)
                        .First()
                })
                .ToList();

            return await _orderRepository.AddAsync(orderLines, CalculatePackageWidth(orderLines));
        }

        private double CalculatePackageWidth(List<OrderLinePostModel> orderLines)
        {
            return orderLines.Sum(
                x => x.ProductType == "Mug"
                    ? x.WidthMm * ((x.Quantity / 4) + (x.Quantity % 4 > 0 ? 1 : 0))
                    : x.WidthMm * x.Quantity);
        }
    }
}
