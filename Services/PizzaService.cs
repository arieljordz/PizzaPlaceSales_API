using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PizzaPlaceSales_API.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly API_DBContext db;
        private readonly IConfiguration configuration;

        public PizzaService(API_DBContext context, IConfiguration configuration)
        {
            this.db = context;
            this.configuration = configuration;
        }

        public async Task<(List<Pizza> Pizzas, int TotalCount)> GetPizzasAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var data = await db.Pizzas
                                 .OrderBy(x => x.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .ToListAsync();

            var totalCount = await db.Pizzas.CountAsync();

            return (data, totalCount);
        }

        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            return await db.Pizzas.FindAsync(id);
        }

        public async Task<ResultDTO> ImportPizzasAsync(IFormFile file)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PizzaDTO>().ToList();

                    if (records.Any())
                    {
                        foreach (var record in records)
                        {
                            var obj = new Pizza
                            {
                                pizza_id = record.pizza_id,
                                pizza_type_id = record.pizza_type_id,
                                size = record.size,
                                price = record.price,
                            };
                            await db.Pizzas.AddAsync(obj);
                        }

                        await db.SaveChangesAsync();

                        result.IsSuccess = true;
                        result.ErrorMessage = "Pizzas imported successfully";
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
