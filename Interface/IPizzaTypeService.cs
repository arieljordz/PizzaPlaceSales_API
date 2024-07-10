using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IPizzaTypeService
    {
        Task<List<PizzaType>> GetPizzaTypesAsync();

        Task<PizzaType> GetPizzaTypeByIdAsync(int id);

        Task<ResultDTO> ImportPizzaTypesAsync(string filePath);
    }
}
