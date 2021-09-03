using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.Api.Data;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ManufacturingDbContext _manufacturingDbContext;

        public OrderRepository(ManufacturingDbContext manufacturingDbContext)
        {
            _manufacturingDbContext = manufacturingDbContext;
        }

        public async Task<Entities.Order> AddAsync(List<OrderLinePostModel> orderLines, double totalPackageWidth)
        {
            Entities.Order order = new Entities.Order
            {
                MinPackageWidth = totalPackageWidth,
                OrderLines = orderLines
                    .Select(x => new Entities.OrderLine
                    {
                        ProductId = x.Id,
                        Quantity = x.Quantity
                    })
                    .ToList()
            };

            _manufacturingDbContext.Orders.Add(order);
            await _manufacturingDbContext.SaveChangesAsync();

            return order;
        }

        public async Task<OrderVM> GetByIdAsync(int orderId)
        {
            return await _manufacturingDbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .Where(x => x.Id == orderId)
                .Select(x => new OrderVM
                {
                    OrderId = x.Id,
                    MinPackageWidth = x.MinPackageWidth,
                    Items = x.OrderLines
                        .Select(ol => new OrderLineVM
                        {
                            ProductType = ol.Product.Type,
                            Quantity = ol.Quantity
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
