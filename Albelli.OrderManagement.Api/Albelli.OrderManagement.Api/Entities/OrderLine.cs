using System.ComponentModel.DataAnnotations;

namespace Albelli.OrderManagement.Api.Entities
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
