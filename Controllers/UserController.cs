using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzaPlaceSales_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly API_DBContext db;
        private readonly IUserService service;

        public UserController(API_DBContext context, IUserService service)
        {
            this.db = context;
            this.service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO data)
        {
            var result = await service.RegisterAsync(data);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            else
            {
                return Ok("User registered successfully");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            var result = await service.LoginAsync(data);
            if (result.IsSuccess == false)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    token = result.Token,
                    expiration = result.TokenValidity,
                    email = data.Email
                });
            }

        }

        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await service.GetUsersAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await service.GetUserByIdAsync(id);
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
