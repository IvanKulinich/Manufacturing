using Albelli.OrderManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Entities.Order> AddAsync(List<OrderLinePostModel> orderLines, double totalPackageWidth);

        Task<OrderVM> GetByIdAsync(int orderId);
    }
}
