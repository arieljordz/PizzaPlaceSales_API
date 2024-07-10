using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PizzaPlaceSales_API.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly API_DBContext db;
        private readonly IConfiguration configuration;

        public OrderDetailService(API_DBContext context, IConfiguration configuration)
        {
            this.db = context;
            this.configuration = configuration;
        }

        public async Task<(List<OrderDetail> Orders, int TotalCount)> GetOrderDetailsAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var orders = await db.OrderDetails
                                 .OrderBy(x => x.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .ToListAsync();

            var totalCount = await db.OrderDetails.CountAsync();

            return (orders, totalCount);
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await db.OrderDetails.FindAsync(id);
        }

        public async Task<ResultDTO> ImportOrderDetailsAsync(IFormFile file)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<OrderDetailDTO>().ToList();

                    if (records.Any())
                    {
                        foreach (var record in records)
                        {
                            var obj = new OrderDetail
                            {
                                order_details_id = record.order_details_id,
                                order_id = record.order_id,
                                pizza_id = record.pizza_id,
                                quantity = record.quantity,
                            };
                            await db.OrderDetails.AddAsync(obj);
                        }

                        await db.SaveChangesAsync();

                        result.IsSuccess = true;
                        result.ErrorMessage = "Orders Details imported successfully";
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
