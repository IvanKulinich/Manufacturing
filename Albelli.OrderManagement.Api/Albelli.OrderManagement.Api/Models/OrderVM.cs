using System.Collections.Generic;

namespace Albelli.OrderManagement.Api.Models
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public IEnumerable<OrderLineVM> Items { get; set; }

        public double MinPackageWidth { get; set; }
    }
}
