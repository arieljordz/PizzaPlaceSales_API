using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PizzaPlaceSales_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly API_DBContext db;
        private readonly IConfiguration configuration;

        public OrderService(API_DBContext context, IConfiguration configuration)
        {
            this.db = context;
            this.configuration = configuration;
        }

        public async Task<(List<Order> Orders, int TotalCount)> GetOrdersAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var orders = await db.Orders
                                 .OrderBy(x => x.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .ToListAsync();

            var totalCount = await db.Orders.CountAsync();

            return (orders, totalCount);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await db.Orders.FindAsync(id);
        }
        public async Task<ResultDTO> ImportOrdersAsync(IFormFile file)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<OrderDTO>().ToList();

                    if (records.Any())
                    {
                        foreach (var record in records)
                        {
                            var obj = new Order
                            {
                                order_id = record.order_id,
                                date = record.date,
                                time = record.time,
                            };
                            await db.Orders.AddAsync(obj);
                        }

                        await db.SaveChangesAsync();

                        result.IsSuccess = true;
                        result.ErrorMessage = "Orders imported successfully";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.ErrorMessage = "No records found in the CSV file";
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Error importing orders: {ex.Message}";
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return result;
        }

    }
}
