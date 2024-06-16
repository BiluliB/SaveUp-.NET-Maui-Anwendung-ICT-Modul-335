namespace SaveUpBackend.DTOs.Requests
{
    public class SavedMoneyCreateDTO
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}
