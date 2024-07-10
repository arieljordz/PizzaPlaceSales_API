using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Interface
{
    public interface IUserService
    {
        Task<ResultDTO> RegisterAsync(UserDTO data);

        Task<ResultDTO> LoginAsync(LoginDTO data);

        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);
    }
}
