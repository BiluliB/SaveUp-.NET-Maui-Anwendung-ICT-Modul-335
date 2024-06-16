using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SaveUpBackend.Models
{
    public class SavedMoney : BaseModel
    {
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

    }
}
