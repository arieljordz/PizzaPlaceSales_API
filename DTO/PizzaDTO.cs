using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.DTO
{
    public class PizzaDTO
    {
        public string? pizza_id { get; set; }

        public string? pizza_type_id { get; set; }

        public string? size { get; set; }

        public decimal price { get; set; }
    }
}
