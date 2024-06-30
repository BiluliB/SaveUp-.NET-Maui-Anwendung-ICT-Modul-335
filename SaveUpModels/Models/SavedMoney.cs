namespace SaveUpModels.Models
{
    /// <summary>
    /// Model for the SavedMoney
    /// </summary>
    public class SavedMoney : BaseModel
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

    }
}
