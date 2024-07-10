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

        //[Authorize]
        [HttpGet]
        [Route("GetPizzaTypes")]
        public async Task<IActionResult> GetPizzaTypes(int page = 1, int pageSize = 10)
        {
            var result = await service.GetPizzaTypesAsync(page, pageSize);

            if (result.PizzaTypes == null || !result.PizzaTypes.Any())
            {
                return NoContent();
            }

            var response = new
            {
                Data = result.PizzaTypes,
                TotalCount = result.TotalCount
            };

            return Ok(response);
        }

        //[Authorize]
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
        public async Task<IActionResult> ImportPizzaTypes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            var result = await service.ImportPizzaTypesAsync(file);
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
