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
    public class OrderController : ControllerBase
    {
        private readonly API_DBContext db;
        private readonly IOrderService service;

        public OrderController(API_DBContext context, IOrderService service)
        {
            this.db = context;
            this.service = service;
        }

        //[Authorize]
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders(int page = 1, int pageSize = 10)
        {
            var result = await service.GetOrdersAsync(page, pageSize);

            if (result.Orders == null || !result.Orders.Any())
            {
                return NoContent();
            }

            var response = new
            {
                Data = result.Orders,
                TotalCount = result.TotalCount
            };

            return Ok(response);
        }


        //[Authorize]
        [HttpGet]
        [Route("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await service.GetOrderByIdAsync(id);
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
        [Route("ImportOrders")]
        public async Task<IActionResult> ImportOrders(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            var result = await service.ImportOrdersAsync(file);
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
