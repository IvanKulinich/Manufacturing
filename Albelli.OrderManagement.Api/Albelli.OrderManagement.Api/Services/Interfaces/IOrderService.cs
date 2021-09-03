using Albelli.OrderManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderVM> GetByIdAsync(int orderId);

        Task<Entities.Order> AddAsync(IEnumerable<OrderLineVM> items);
    }
}
