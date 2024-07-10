using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.DTO
{
    public class PizzaTypeDTO
    {
        public string? pizza_type_id { get; set; }

        public string? name { get; set; }

        public string? category { get; set; }

        public string? ingredients { get; set; }
    }
}
