using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.DTO
{
    public class OrderDetailDTO
    {

        public int order_details_id { get; set; }

        public int order_id { get; set; }

        public string? pizza_id { get; set; }

        public int quantity { get; set; }
    }
}
