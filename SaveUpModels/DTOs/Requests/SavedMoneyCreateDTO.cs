namespace SaveUpModels.DTOs.Requests
{
    public class SavedMoneyCreateDTO
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
