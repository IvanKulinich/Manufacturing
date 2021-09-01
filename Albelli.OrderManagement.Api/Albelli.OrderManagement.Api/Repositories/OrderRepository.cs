using System;
using System.Collections.Generic;
using System.Linq;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class OrderRepository
    {
        private static readonly IList<Order> _orders = new List<Order>
        {
            new Order { OrderId = 1, MinPackageWidth = 19, Items = new List<OrderLine>
            {
                new OrderLine { ProductType = "PhotoBook", Quantity = 1 }
            }}
        };

        public void Add(Order order)
        {
            order.OrderId = _orders.Max(o => o.OrderId) + 1;
            _orders.Add(order);
        }

        public Order GetOrder(int orderId)
        {
            return _orders.FirstOrDefault(x => x.OrderId == orderId);
        }
    }
}
