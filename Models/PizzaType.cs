using System.ComponentModel.DataAnnotations;

namespace PizzaPlaceSales_API.Models
{
    public class PizzaType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? pizza_type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string? name { get; set; }

        [Required]
        [StringLength(50)]
        public string? category { get; set; }

        [Required]
        [StringLength(500)]
        public string? ingredients { get; set; }

    }
}
