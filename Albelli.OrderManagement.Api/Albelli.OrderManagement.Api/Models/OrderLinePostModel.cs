namespace Albelli.OrderManagement.Api.Models
{
    public class OrderLinePostModel
    {
        public int Id { get; set; }

        public string ProductType { get; set; }

        public double WidthMm { get; set; }

        public int Quantity { get; set; }
    }
}
