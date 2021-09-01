using System;
using System.Collections.Generic;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class ProductInfoRepository
    {
        private static readonly IDictionary<string, ProductInfo> _productInfo = new Dictionary<string, ProductInfo>()
        {
            { "PhotoBook", new ProductInfo { WidthMm = 19 } },
            { "Calendar", new ProductInfo { WidthMm = 10 } },
            { "Canvas", new ProductInfo { WidthMm = 16 } },
            { "Cards", new ProductInfo { WidthMm = 4.7 } },
            { "Mug", new ProductInfo { WidthMm = 94 } }
        };

        public ProductInfo Get(string productType)
        {
            if (!_productInfo.ContainsKey(productType))
            {
                throw new Exception($"No information available for product type {productType}.", null);
            }

            var info = _productInfo[productType];

            return new ProductInfo
            {
                ProductType = productType,
                WidthMm = info.WidthMm
            };
        }
    }
}
