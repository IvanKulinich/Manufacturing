using System.ComponentModel.DataAnnotations;

namespace Albelli.OrderManagement.Api.Models
{
    public class OrderLineVM
    {
        public string ProductType { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
