using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public required string ShopNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AreaSqFt { get; set; }
        public required string Floor { get; set; }
        public bool IsOccupied { get; set; }
    }
}
