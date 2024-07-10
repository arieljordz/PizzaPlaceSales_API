using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;

namespace PizzaPlaceSales_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly API_DBContext db;
        private readonly IPizzaService service;

        public PizzaController(API_DBContext context, IPizzaService service)
        {
            this.db = context;
            this.service = service;
        }

        [Authorize]
        [HttpGet]
        [Route("GetPizzas")]
        public async Task<IActionResult> GetPizzas()
        {
            var result = await service.GetPizzasAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPizzaById")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            var result = await service.GetPizzaByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }

        }

        //[Authorize]
        [HttpPost]
        [Route("ImportPizzas")]
        public async Task<IActionResult> ImportPizzas(string filePath)
        {
            var result = await service.ImportPizzasAsync(filePath);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }

        }

    }
}
