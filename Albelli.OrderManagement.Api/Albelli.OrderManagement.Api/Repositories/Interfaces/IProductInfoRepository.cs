using Albelli.OrderManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Repositories.Interfaces
{
    public interface IProductInfoRepository
    {
        Task<List<ProductInfo>> GetItemsByTypes(IEnumerable<string> productTypes);
    }
}
