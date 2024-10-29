using OrdersFiltration.Enums;

namespace OrdersFiltration.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public float Weight { get; set; }
        public District District { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
