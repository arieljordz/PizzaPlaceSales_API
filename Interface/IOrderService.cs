using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IOrderService
    {
        Task<(List<Order> Orders, int TotalCount)> GetOrdersAsync(int page, int pageSize);

        Task<Order> GetOrderByIdAsync(int id);

        Task<ResultDTO> ImportOrdersAsync(IFormFile file);
    }
}
