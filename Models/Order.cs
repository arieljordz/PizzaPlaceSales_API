using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int order_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime time { get; set; }

    }
}
