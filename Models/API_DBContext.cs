using Microsoft.EntityFrameworkCore;

namespace PizzaPlaceSales_API.Models
{
    public class API_DBContext : DbContext
    {
        public API_DBContext(DbContextOptions<API_DBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }
    }
}
