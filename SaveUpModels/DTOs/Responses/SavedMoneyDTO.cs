﻿namespace SaveUpModels.DTOs.Responses
{
    /// <summary>
    /// DTO for the SavedMoney
    /// </summary>
    public class SavedMoneyDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
