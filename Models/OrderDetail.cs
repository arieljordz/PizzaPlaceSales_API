using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.Models
{
    public class OrderDetail
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int order_details_id { get; set; }

        [Required]
        public int order_id { get; set; }

        [Required]
        [StringLength(50)]
        public string? pizza_id { get; set; }

        [Required]
        public int quantity { get; set; }
    }
}
