using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IPizzaService
    {
        Task<List<Pizza>> GetPizzasAsync();

        Task<Pizza> GetPizzaByIdAsync(int id);

        Task<ResultDTO> ImportPizzasAsync(string filePath);
    }
}
