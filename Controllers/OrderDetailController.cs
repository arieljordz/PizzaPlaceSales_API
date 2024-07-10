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
    public class OrderDetailController : ControllerBase
    {
        private readonly API_DBContext db;
        private readonly IOrderDetailService service;

        public OrderDetailController(API_DBContext context, IOrderDetailService service)
        {
            this.db = context;
            this.service = service;
        }

        //[Authorize]
        [HttpGet]
        [Route("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(int page = 1, int pageSize = 10)
        {
            var result = await service.GetOrderDetailsAsync(page, pageSize);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        //[Authorize]
        [HttpGet]
        [Route("GetOrderDetailById")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var result = await service.GetOrderDetailByIdAsync(id);
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
        [Route("ImportOrderDetails")]
        public async Task<IActionResult> ImportOrderDetails(IFormFile file)
        {
            var result = await service.ImportOrderDetailsAsync(file);
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
