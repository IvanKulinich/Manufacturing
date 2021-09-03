using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories.Interfaces;
using Albelli.OrderManagement.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void PackageWidthTest()
        {
            List<OrderLinePostModel> testLines = new List<OrderLinePostModel>
            {
                new OrderLinePostModel
                {
                    Id = 2,
                    WidthMm = 10,
                    ProductType = "Calendar",
                    Quantity = 2
                }
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockProductInfoRepository = new Mock<IProductInfoRepository>();

            Type type = typeof(OrderService);
            var orderService = Activator.CreateInstance(type, mockOrderRepository.Object, mockProductInfoRepository.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "CalculatePackageWidth" && x.IsPrivate)
                .First();

            var result = method.Invoke(orderService, new object[]
            {
                testLines
            });

            Assert.Equal(20.0, result);
        }

        [Fact]
        public void MugPackageWidthTest()
        {
            List<OrderLinePostModel> testLines = new List<OrderLinePostModel>
            {
                new OrderLinePostModel
                {
                    Id = 5,
                    WidthMm = 94,
                    ProductType = "Mug",
                    Quantity = 5
                }
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockProductInfoRepository = new Mock<IProductInfoRepository>();

            Type type = typeof(OrderService);
            var orderService = Activator.CreateInstance(type, mockOrderRepository.Object, mockProductInfoRepository.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "CalculatePackageWidth" && x.IsPrivate)
                .First();

            var result = method.Invoke(orderService, new object[]
            {
                testLines
            });

            Assert.Equal(188.0, result);
        }
    }
}
