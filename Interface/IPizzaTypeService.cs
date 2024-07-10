using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IPizzaTypeService
    {
        Task<(List<PizzaType> PizzaTypes, int TotalCount)> GetPizzaTypesAsync(int page, int pageSize);

        Task<PizzaType> GetPizzaTypeByIdAsync(int id);

        Task<ResultDTO> ImportPizzaTypesAsync(IFormFile file);
    }
}
