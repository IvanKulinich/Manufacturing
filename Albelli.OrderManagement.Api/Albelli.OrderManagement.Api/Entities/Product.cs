using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Albelli.OrderManagement.Api.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public double Width { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
