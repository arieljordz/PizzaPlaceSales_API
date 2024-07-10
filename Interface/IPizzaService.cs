using Microsoft.AspNetCore.Mvc;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IPizzaService
    {
        Task<(List<Pizza> Pizzas, int TotalCount)> GetPizzasAsync(int page, int pageSize);

        Task<Pizza> GetPizzaByIdAsync(int id);

        Task<ResultDTO> ImportPizzasAsync(IFormFile file);
    }
}
