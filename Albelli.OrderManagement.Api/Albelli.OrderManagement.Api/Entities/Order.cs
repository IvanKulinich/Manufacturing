using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Albelli.OrderManagement.Api.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public double MinPackageWidth { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
