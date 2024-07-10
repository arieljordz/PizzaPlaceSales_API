using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PizzaPlaceSales_API.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? pizza_id { get; set; }

        [Required]
        [StringLength(50)]
        public string? pizza_type_id { get; set; }

        [Required]
        [StringLength(1)]
        public string? size { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }
    }
}
