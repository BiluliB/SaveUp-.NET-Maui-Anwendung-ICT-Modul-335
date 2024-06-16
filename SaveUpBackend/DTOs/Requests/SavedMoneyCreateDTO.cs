namespace SaveUpBackend.DTOs.Requests
{
    public class SavedMoneyCreateDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
