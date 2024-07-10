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

        public async Task<List<PizzaType>> GetPizzaTypesAsync()
        {
            return await db.PizzaTypes.ToListAsync();
        }

        public async Task<PizzaType> GetPizzaTypeByIdAsync(int id)
        {
            return await db.PizzaTypes.FindAsync(id);
        }
        public async Task<ResultDTO> ImportPizzaTypesAsync(string filePath)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (var reader = new StreamReader(filePath))
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
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
