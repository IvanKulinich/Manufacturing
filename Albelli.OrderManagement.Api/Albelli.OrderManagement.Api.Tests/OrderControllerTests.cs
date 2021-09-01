using Albelli.OrderManagement.Api.Controllers;
using Albelli.OrderManagement.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void PackageWidthTest()
        {
            var orderController = new OrdersController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = "Calendar", Quantity = 2});

            var res = orderController.PackageWidth(lines);

            Assert.Equal(20, res);
        }

        [Fact]
        public void MugPackageWidthTest()
        {
            var orderController = new OrdersController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = "Mug", Quantity = 5 });

            var res = orderController.PackageWidth(lines);

            Assert.Equal(188, res);
        }
    }
}
