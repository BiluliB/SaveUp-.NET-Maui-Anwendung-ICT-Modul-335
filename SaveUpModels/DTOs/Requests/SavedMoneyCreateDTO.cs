namespace SaveUpModels.DTOs.Requests
{
    /// <summary>
    /// DTO for the creation of a SavedMoney
    /// </summary>
    public class SavedMoneyCreateDTO
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
