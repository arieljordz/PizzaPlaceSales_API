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
    public class PizzaTypeController : ControllerBase
    {
        private readonly API_DBContext db;
        private readonly IPizzaTypeService service;

        public PizzaTypeController(API_DBContext context, IPizzaTypeService service)
        {
            this.db = context;
            this.service = service;
        }

        [Authorize]
        [HttpGet]
        [Route("GetPizzaTypes")]
        public async Task<IActionResult> GetPizzaTypes()
        {
            var result = await service.GetPizzaTypesAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPizzaTypeById")]
        public async Task<IActionResult> GetPizzaTypeById(int id)
        {
            var result = await service.GetPizzaTypeByIdAsync(id);
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
        [Route("ImportPizzaTypes")]
        public async Task<IActionResult> ImportPizzaTypes(string filePath)
        {
            var result = await service.ImportPizzaTypesAsync(filePath);
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
