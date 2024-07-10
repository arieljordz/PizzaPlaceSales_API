using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaPlaceSales_API.Services
{
    public class UserService : IUserService
    {
        private readonly API_DBContext db;
        private readonly IConfiguration configuration;

        public UserService(API_DBContext context, IConfiguration configuration)
        {
            this.db = context;
            this.configuration = configuration;
        }
        public async Task<ResultDTO> RegisterAsync(UserDTO data)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                if (data == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User data is null";
                }
                else
                {
                    var obj = new User
                    {
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Email = data.Email,
                        Password = data.Password
                    };

                    await db.Users.AddAsync(obj);
                    await db.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.ErrorMessage = "User registered successfully";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<ResultDTO> LoginAsync(LoginDTO data)
        {
            ResultDTO result = new ResultDTO();

            var user = await db.Users.FirstOrDefaultAsync(x => x.Email == data.Email && x.Password == data.Password);
            if (user == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "User data is null";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireMinutes"])),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                result.IsSuccess = true;
                result.Token = jwtToken;
                result.TokenValidity = token.ValidTo;
            }

            return result;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }
    }
}
