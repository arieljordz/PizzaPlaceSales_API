using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PizzaPlaceSales_API.DTO;
using PizzaPlaceSales_API.Interface;
using PizzaPlaceSales_API.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PizzaPlaceSales_API.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly API_DBContext db;
        private readonly IConfiguration configuration;

        public PizzaTypeService(API_DBContext context, IConfiguration configuration)
        {
            this.db = context;
            this.configuration = configuration;
        }

        public async Task<(List<PizzaType> PizzaTypes, int TotalCount)> GetPizzaTypesAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var data = await db.PizzaTypes
                                 .OrderBy(x => x.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .ToListAsync();

            var totalCount = await db.PizzaTypes.CountAsync();

            return (data, totalCount);
        }

        public async Task<PizzaType> GetPizzaTypeByIdAsync(int id)
        {
            return await db.PizzaTypes.FindAsync(id);
        }

        public async Task<ResultDTO> ImportPizzaTypesAsync(IFormFile file)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PizzaTypeDTO>().ToList();

                    if (records.Any())
                    {
                        foreach (var record in records)
                        {
                            var obj = new PizzaType
                            {
                                pizza_type_id = record.pizza_type_id,
                                name = record.name,
                                category = record.category,
                                ingredients = record.ingredients,
                            };
                            await db.PizzaTypes.AddAsync(obj);
                        }

                        await db.SaveChangesAsync();

                        result.IsSuccess = true;
                        result.ErrorMessage = "PizzaTypes imported successfully";
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
