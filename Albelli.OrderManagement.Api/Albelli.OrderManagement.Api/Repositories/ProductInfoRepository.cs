using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.Api.Data;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private readonly ManufacturingDbContext _manufacturingDbContext;

        public ProductInfoRepository(ManufacturingDbContext manufacturingDbContext)
        {
            _manufacturingDbContext = manufacturingDbContext;
        }

        public async Task<List<ProductInfo>> GetItemsByTypes(IEnumerable<string> productTypes)
        {
            return await _manufacturingDbContext.Products
                .Where(x => productTypes.Contains(x.Type))
                .Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductType = x.Type,
                    WidthMm = x.Width
                })
                .ToListAsync();
        }
    }
}
