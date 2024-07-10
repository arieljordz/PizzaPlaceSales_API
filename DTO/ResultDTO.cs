namespace PizzaPlaceSales_API.DTO
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }

        public DateTime? TokenValidity { get; set; }

    }
}
