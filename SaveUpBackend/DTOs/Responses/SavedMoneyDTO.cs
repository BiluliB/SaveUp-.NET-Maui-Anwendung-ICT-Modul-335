﻿namespace SaveUpBackend.DTOs.Responses
{
    public class SavedMoneyDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}
