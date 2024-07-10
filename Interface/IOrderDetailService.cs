using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetOrderDetailsAsync(int page, int pageSize);

        Task<OrderDetail> GetOrderDetailByIdAsync(int id);

        Task<ResultDTO> ImportOrderDetailsAsync(IFormFile file);
    }
}
