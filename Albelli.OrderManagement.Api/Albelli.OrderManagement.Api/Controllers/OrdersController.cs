using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private OrderRepository _orderRepository;
        private ProductInfoRepository _productInfoRepository;

        public OrdersController()
        {
            _orderRepository = new OrderRepository();
            _productInfoRepository = new ProductInfoRepository();
        }

        [HttpPost("orders/place")]
        public ActionResult PlaceOrder([FromBody] IEnumerable<OrderLine> items)
        {
            try
            {
                var orderLines = items.ToList();
                var order = new Order { Items = orderLines, MinPackageWidth = PackageWidth(orderLines) };

                _orderRepository.Add(order);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("order/{orderId}/retrieve")]
        public async Task<IActionResult> RetrieveOrder(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrder(orderId);

                if (order == null)
                {
                    throw new ArgumentException(orderId.ToString());
                }

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public double PackageWidth(IEnumerable<OrderLine> items)
        {
            double pw = 0;

            foreach (var item in items)
            {
                var t = item.ProductType;
                var q = item.Quantity;

                var info = _productInfoRepository.Get(t);

                pw += info.WidthMm * q;
            }

            return pw;
        }
    }
}
