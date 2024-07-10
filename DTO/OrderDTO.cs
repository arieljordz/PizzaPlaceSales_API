using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.DTO
{
    public class OrderDTO
    {

        public int order_id { get; set; }

        public DateTime date { get; set; }

        public DateTime time { get; set; }
    }
}
