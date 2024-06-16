﻿namespace SaveUpBackend.DTOs.Responses
{
    public class SavedMoneyDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
